using Microsoft.Extensions.Logging;
using SmartLock.CQRS;
using SmartLock.CQRS.Command;

namespace SmartLock.Service.Doors
{
    public class DoorService : IDoorService
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogger<DoorService> _logger;
        public DoorService(ICommandDispatcher commandDispatcher, ILogger<DoorService> logger)
        {
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }

        public async Task OpenDoorAsync(long doorId, long userId, long[] roleIds, string comments)
        {
            if (doorId == 0 || userId == 0)
            {
                _logger.LogError($"Invalid input door {doorId}, user {userId}");
                throw new ArgumentException("Invalid input");
            }

            if (roleIds == null || !roleIds.Any())
            {
                _logger.LogError($"Insufficient permission to enter the door {doorId}, user {userId}");
                throw new UnauthorizedAccessException($"Insufficient permission to access this door");
            }

            await _commandDispatcher.DispatchAsync(new OpenDoorCommand { DoorId = doorId, UserId = userId, RoleIds = roleIds, Comments = comments });
        }
    }
}
