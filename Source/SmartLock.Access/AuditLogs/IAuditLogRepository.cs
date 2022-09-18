using SmartLock.Model;

namespace SmartLock.Access.AuditLogs
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLog>> GetByUserIdAsync(long userId);
        Task AddAsync(AuditLog auditLog);
        Task SaveChangesAsync();
    }
}
