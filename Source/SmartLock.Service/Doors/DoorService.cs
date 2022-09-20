using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartLock.CQRS;
using SmartLock.CQRS.Command;
using SmartLock.CQRS.Query;
using SmartLock.CQRS.QueryResult;
using SmartLock.Model.Dto;

namespace SmartLock.Service.Doors
{
    public class DoorService : IDoorService
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<DoorService> _logger;
        private readonly IMapper _mapper;
        public DoorService(ICommandDispatcher commandDispatcher, ILogger<DoorService> logger, IQueryDispatcher queryDispatcher, IMapper mapper)
        {
            _commandDispatcher = commandDispatcher;
            _logger = logger;
            _queryDispatcher = queryDispatcher;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoorDto>> GetByUserIdAsync(long userId)
        {
            var doorsQueryResult = await _queryDispatcher.DispatchAsync<DoorsByUserIdQuery, DoorsByUserIdQueryResult>(new DoorsByUserIdQuery
            {
                UserId = userId
            });

            return _mapper.Map<List<DoorDto>>(doorsQueryResult.Doors);
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
