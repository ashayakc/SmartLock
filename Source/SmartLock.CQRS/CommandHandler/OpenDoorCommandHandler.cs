using Microsoft.Extensions.Logging;
using SmartLock.Access.Doors;
using SmartLock.Access.Users;
using SmartLock.Common.Exceptions;
using SmartLock.CQRS.Command;
using SmartLock.CQRS.Event;

namespace SmartLock.CQRS.CommandHandler
{
    public class OpenDoorCommandHandler : ICommandHandler<OpenDoorCommand>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IDoorRepository _doorRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<OpenDoorCommandHandler> _logger;
        public OpenDoorCommandHandler(ICommandDispatcher commandDispatcher, IDoorRepository doorRepository, IUserRepository userRepository, ILoggerFactory loggerFactory)
        {
            _commandDispatcher = commandDispatcher;
            _doorRepository = doorRepository;
            _userRepository = userRepository;
            _logger = loggerFactory.CreateLogger<OpenDoorCommandHandler>();
        }

        public async Task ExecuteAsync(OpenDoorCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var door = await _doorRepository.GetByIdAsync(command.DoorId);
            if (door == null)
            {
                _logger.LogError($"Invalid doorId {command.DoorId} supplied by user {command.UserId}");
                throw new ArgumentException("Invalid door");
            }

            //What if User A tries to access the door of different office which he has not having access to?
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if(user == null 
                || user.UserOfficeRoleMappings == null 
                || user.UserOfficeRoleMappings.Count == 0
                || !user.UserOfficeRoleMappings.Any(_ => _.OfficeId == door.OfficeId))
            {
                await RaiseDoorFailedEvent(command.DoorId, command.UserId, door.OfficeId, command.Comments);
            }

            var isPermitted = command.RoleIds.Intersect(door.DoorRoleMappings.Select(m => m.RoleId)).Any();
            if(!isPermitted)
            {
                await RaiseDoorFailedEvent(command.DoorId, command.UserId, door.OfficeId, command.Comments);
            }

            //We can make some API calls to the hardware to open the door here.
            //Raising an event so that it can be handled by anyone who is interested like Sending email, notfication, auditing ..etc
            await _commandDispatcher.DispatchAsync(new DoorOpenedSuccessEvent { DoorId = command.DoorId, UserId = command.UserId, OfficeId = door.OfficeId, Comments = command.Comments });
        }

        private async Task RaiseDoorFailedEvent(long doorId, long userId, long officeId, string comments)
        {
            _logger.LogWarning($"Attempt for an unauthorized entry by user {userId} on door {doorId}");
            await _commandDispatcher.DispatchAsync(new DoorOpenedFailedEvent { DoorId = doorId, UserId = userId, OfficeId = officeId, Comments = comments });
            throw new SmartLockApiException("You are not authorized to access this door. Please contact the administrator");
        }
    }
}
