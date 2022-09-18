using SmartLock.Model;

namespace SmartLock.CQRS.QueryResult
{
    public class AuditLogByUserIdQueryResult : IQueryResult
    {
        public AuditLogByUserIdQueryResult(IEnumerable<AuditLog> auditLogs)
        {
            AuditLogs = auditLogs;
        }

        public IEnumerable<AuditLog> AuditLogs { get; set; }
    }
}
