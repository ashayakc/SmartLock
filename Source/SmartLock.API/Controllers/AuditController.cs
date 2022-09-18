using Microsoft.AspNetCore.Mvc;
using SmartLock.Model.Dto;
using SmartLock.Service.AuditLogs;

namespace SmartLock.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuditController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;
        public AuditController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet, Route("users/{userId}/audits")]
        public Task<IEnumerable<AuditLogDto>> GetByUserIdAsync(long userId)
        {
            return _auditLogService.GetAuditLogsAsync(userId);
        }
    }
}