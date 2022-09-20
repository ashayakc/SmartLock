using Microsoft.Extensions.DependencyInjection;
using SmartLock.Access.AuditLogs;
using SmartLock.Access.DoorRoleMappings;
using SmartLock.Access.Doors;
using SmartLock.Access.UserOfficeRoleMappings;
using SmartLock.Access.Users;

namespace SmartLock.Access
{
    public static class SmartLockRepositoryRegister
    {
        public static void RegisterSmartLockRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDoorRepository, DoorRepository>();
            services.AddScoped<IUserOfficeRoleMappingRepository, UserOfficeRoleMappingRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IDoorRoleMappingRepository, DoorRoleMappingRepository>();
        }
    }
}
