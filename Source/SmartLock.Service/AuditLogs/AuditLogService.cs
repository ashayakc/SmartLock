using AutoMapper;
using SmartLock.CQRS;
using SmartLock.CQRS.Query;
using SmartLock.CQRS.QueryResult;
using SmartLock.Model.Dto;

namespace SmartLock.Service.AuditLogs
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IMapper _mapper;
        public AuditLogService(IQueryDispatcher queryDispatcher, IMapper mapper)
        {
            _queryDispatcher = queryDispatcher;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(long userId)
        {
            var auditLogs = await _queryDispatcher.DispatchAsync<AuditLogByUesrIdQuery, AuditLogByUserIdQueryResult>(new AuditLogByUesrIdQuery
            {
                UserId = userId
            });

            return _mapper.Map<List<AuditLogDto>>(auditLogs.AuditLogs);
        }
    }
}
