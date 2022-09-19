using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SmartLock.Access.Doors;
using SmartLock.Access.Users;
using SmartLock.Common.Exceptions;
using SmartLock.CQRS.Command;
using SmartLock.CQRS.CommandHandler;
using SmartLock.CQRS.Event;
using SmartLock.Model;

namespace SmartLock.CQRS.Unit.Test
{
    public class OpenDoorCommandHandlerTest
    {
        private readonly Mock<ICommandDispatcher> _commandDispatcher;
        private readonly Mock<IDoorRepository> _doorRepository;
        private readonly Mock<IUserRepository> _userRepository;
        public OpenDoorCommandHandlerTest()
        {
            _commandDispatcher = new Mock<ICommandDispatcher>();
            _doorRepository = new Mock<IDoorRepository>();
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task ExecuteAsync_ThrowException_WhenCommandIsNull()
        {
            var handler = GetOpenDoorCommandHandler();
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.ExecuteAsync(null as OpenDoorCommand));
        }

        [Fact]
        public async Task ExecuteAsync_ThrowException_WhenDoorIsInvalid()
        {
            _doorRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(null as Door);

            var handler = GetOpenDoorCommandHandler();
            await Assert.ThrowsAsync<ArgumentException>(async () => await handler.ExecuteAsync(new OpenDoorCommand { }));
        }

        [Fact]
        public async Task ExecuteAsync_ThrowException_WhenUserIdIsInvalid()
        {
            _doorRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Door { Id = 1, Name = "door1", OfficeId = 1 });

            _userRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(null as User);

            var handler = GetOpenDoorCommandHandler();
            await Assert.ThrowsAsync<SmartLockApiException>(async () => await handler.ExecuteAsync(new OpenDoorCommand { }));

            _commandDispatcher
                .Verify(x => x.DispatchAsync(It.IsAny<DoorOpenedFailedEvent>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ThrowException_WhenUserRoleMappingNotPresent()
        {
            _doorRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Door { Id = 1, Name = "door1", OfficeId = 1 });

            _userRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new User { Id = 1 });

            var handler = GetOpenDoorCommandHandler();
            await Assert.ThrowsAsync<SmartLockApiException>(async () => await handler.ExecuteAsync(new OpenDoorCommand { }));

            _commandDispatcher
                .Verify(x => x.DispatchAsync(It.IsAny<DoorOpenedFailedEvent>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ThrowException_WhenUserDoNotHavePermissionToAccessDoor()
        {
            _doorRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Door
                {
                    Id = 1,
                    Name = "door1",
                    OfficeId = 1,
                    DoorRoleMappings = new List<DoorRoleMapping>
                    {
                        new DoorRoleMapping
                        {
                            DoorId = 1,
                            RoleId = 1
                        }
                    }
                });

            _userRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new User 
                { 
                    Id = 1,
                    UserOfficeRoleMappings = new List<UserOfficeRoleMapping>
                    {
                        new UserOfficeRoleMapping
                        {
                            UserId = 1,
                            RoleId = 2
                        }
                    }
                });

            var handler = GetOpenDoorCommandHandler();
            await Assert.ThrowsAsync<SmartLockApiException>(async () => await handler.ExecuteAsync(new OpenDoorCommand { }));

            _commandDispatcher
                .Verify(x => x.DispatchAsync(It.IsAny<DoorOpenedFailedEvent>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldOpenDoor_WhenUserHavePermissionToAccessDoor()
        {
            _doorRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Door
                {
                    Id = 1,
                    Name = "door1",
                    OfficeId = 1,
                    DoorRoleMappings = new List<DoorRoleMapping>
                    {
                        new DoorRoleMapping
                        {
                            DoorId = 1,
                            RoleId = 1
                        }
                    }
                });

            _userRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    UserOfficeRoleMappings = new List<UserOfficeRoleMapping>
                    {
                        new UserOfficeRoleMapping
                        {
                            UserId = 1,
                            RoleId = 1
                        }
                    }
                });

            var handler = GetOpenDoorCommandHandler();
            await Assert.ThrowsAsync<SmartLockApiException>(async () => await handler.ExecuteAsync(new OpenDoorCommand { }));

            _commandDispatcher
                .Verify(x => x.DispatchAsync(It.IsAny<DoorOpenedFailedEvent>()), Times.Once);
        }

        private OpenDoorCommandHandler GetOpenDoorCommandHandler()
        {
            return new OpenDoorCommandHandler(_commandDispatcher.Object, _doorRepository.Object, _userRepository.Object, new NullLoggerFactory());
        }
    }
}
