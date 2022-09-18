using Microsoft.Extensions.DependencyInjection;
using SmartLock.CQRS.Command;
using SmartLock.CQRS.CommandHandler;
using SmartLock.CQRS.Event;
using SmartLock.CQRS.EventHandler;

namespace SmartLock.CQRS
{
    public static class CommandServiceRegister
    {
        public static void RegisterCommandServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<ICommandHandler<OpenDoorCommand>, OpenDoorCommandHandler>();
            services.AddScoped<ICommandHandler<DoorOpenedSuccessEvent>, AuditEventHandler>();
            services.AddScoped<ICommandHandler<DoorOpenedFailedEvent>, AuditEventHandler>();
        }
    }
}
