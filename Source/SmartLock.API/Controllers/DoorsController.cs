using Microsoft.AspNetCore.Mvc;
using SmartLock.API.Authorization;
using SmartLock.Service.Doors;

namespace SmartLock.Controllers
{
    [ApiController]
    [Route("api/doors")]
    [ApiVersion("1")]
    public class DoorsController : ControllerBase
    {
        private readonly IDoorService _doorService;
        private readonly IClaimsHandler _claimsHandler;
        private readonly ILogger<DoorsController> _logger;
        public DoorsController(IDoorService doorService, IClaimsHandler claimsHandler, ILoggerFactory logger)
        {
            _doorService = doorService;
            _claimsHandler = claimsHandler;
            _logger = logger.CreateLogger<DoorsController>();
        }

        [HttpPost, Route("{doorId}/open")]
        public async Task<IActionResult> OpenAsync(long doorId, [FromBody] string comments)
        {
            _logger.LogInformation($"Trying to open door for {doorId}");
            await _doorService.OpenDoorAsync(doorId, _claimsHandler.GetUserId(User.Claims), _claimsHandler.GetRoleIds(User.Claims), comments);
            return Ok("Door opened successfully");
        }
    }
}