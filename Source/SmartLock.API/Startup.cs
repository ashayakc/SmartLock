using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SmartLock.Access;
using SmartLock.API.Authorization;
using SmartLock.API.Configuration;
using SmartLock.API.Middlewares;
using SmartLock.CQRS;
using SmartLock.Model.Mappings;
using SmartLock.Service;

namespace SmartLock.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = API.Configuration.ConfigurationProvider.GetConfig(Configuration);
            var appConfig = new AppConfig(configuration);
            services.AddSingleton(appConfig);

            services.RegisterCommandServices();
            services.RegisterQueryServices();
            services.RegisterSmartLockServices();
            services.RegisterSmartLockRepositories();
            services.AddScoped<IClaimsHandler, ClaimsHandler>();

            services.AddApiVersioning(config =>
            {
                config.ApiVersionReader = new QueryStringApiVersionReader("version");
                config.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            services.AddDbContext<SmartLockDbContext>(options =>
                options.UseSqlServer(appConfig.DbConnectionString));

            services.AddJwtAuthentication();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.OperationFilter<ApiOperationFilter>();
            });
            services.AddHealthChecks()
               .AddCheck("ping",
                            () => HealthCheckResult.Healthy(),
                            tags: new[] { "ready" });

            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<PermissionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/ping", new HealthCheckOptions
                {
                    Predicate = (check) => check.Tags.Contains("ready"),
                    ResponseWriter = (context, result) => context.Response.WriteAsync("pong")
                });
                endpoints.MapControllers();
            });
            loggerFactory.AddFile($@"{Directory.GetCurrentDirectory()}\logs\logs.txt");
        }
    }
}
