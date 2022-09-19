using SmartLock.Access.AuditLogs;
using SmartLock.CQRS.Event;

namespace SmartLock.CQRS.EventHandler
{
    //Here i made use of the ICommandHandler itself to chain the events. Ideally we can have IEventHandler and handle it separately in future.
    public class AuditEventHandler : 
        ICommandHandler<DoorOpenedSuccessEvent>,
        ICommandHandler<DoorOpenedFailedEvent>
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public AuditEventHandler(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task ExecuteAsync(DoorOpenedSuccessEvent command)
        {
            await _auditLogRepository.AddAsync(new Model.AuditLog
            {
                UserId = command.UserId,
                Comments = command.Comments,
                DoorId = command.DoorId,
                OfficeId = command.OfficeId,
                EventTime = DateTime.UtcNow,
                Status = 1
            });
            await _auditLogRepository.SaveChangesAsync();
        }

        public async Task ExecuteAsync(DoorOpenedFailedEvent command)
        {
            await _auditLogRepository.AddAsync(new Model.AuditLog
            {
                UserId = command.UserId,
                Comments = command.Comments,
                DoorId = command.DoorId,
                OfficeId = command.OfficeId,
                EventTime = DateTime.UtcNow,
                Status = 0
            });
            await _auditLogRepository.SaveChangesAsync();
        }
    }
}
