using Microsoft.Extensions.DependencyInjection;
using SmartLock.DataLogic.Users;
using SmartLock.Service.AuditLogs;
using SmartLock.Service.Doors;

namespace SmartLock.Service
{
    public static class SmartLockServiceRegister
    {
        public static void RegisterSmartLockServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDoorService, DoorService>();
            services.AddScoped<IAuditLogService, AuditLogService>();
        }
    }
}
