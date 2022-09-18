using SmartLock.Access.AuditLogs;
using SmartLock.CQRS.Query;
using SmartLock.CQRS.QueryResult;
using SmartLock.Model;

namespace SmartLock.CQRS.QueryHandler
{
    public class AuditLogByUserIdQueryHandler : IQueryHandler<AuditLogByUesrIdQuery, AuditLogByUserIdQueryResult>
    {
        private readonly IAuditLogRepository _auditLogRepository;
        public AuditLogByUserIdQueryHandler(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AuditLogByUserIdQueryResult> RetrieveAsync(AuditLogByUesrIdQuery query)
        {
            var auditLogs = await _auditLogRepository.GetByUserIdAsync(query.UserId);
            if(!auditLogs.Any())
            {
                return new AuditLogByUserIdQueryResult(new List<AuditLog>());
            }
            return new AuditLogByUserIdQueryResult(auditLogs);
        }
    }
}
