using SmartLock.Model.Dto;

namespace SmartLock.Service.AuditLogs
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(long userId);
    }
}
