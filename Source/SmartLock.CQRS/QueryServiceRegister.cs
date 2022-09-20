using Microsoft.Extensions.DependencyInjection;
using SmartLock.CQRS.Query;
using SmartLock.CQRS.QueryHandler;
using SmartLock.CQRS.QueryResult;

namespace SmartLock.CQRS
{
    public static class QueryServiceRegister
    {
        public static void RegisterQueryServices(this IServiceCollection services)
        {
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<IQueryHandler<AuditLogByUesrIdQuery, AuditLogByUserIdQueryResult>, AuditLogByUserIdQueryHandler>();
            services.AddScoped<IQueryHandler<DoorsByUserIdQuery, DoorsByUserIdQueryResult>, DoorsByUserIdQueryHandler>();
        }
    }
}
