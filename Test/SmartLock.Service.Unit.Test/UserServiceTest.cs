using Moq;
using SmartLock.Access.Users;
using SmartLock.Common.Exceptions;
using SmartLock.DataLogic.Users;
using SmartLock.Model;
using SmartLock.Model.Dto;

namespace SmartLock.Service.Unit.Test
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        public UserServiceTest()
        {
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task AuthenticateAsync_ThrowsException_WhenInputIsNull()
        {
            var service = new UserService(_userRepository.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.AuthenticateAsync(null as UserCredentialDto));
        }

        [Fact]
        public async Task AuthenticateAsync_ThrowsException_WhenUserNameIsNull()
        {
            var service = new UserService(_userRepository.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.AuthenticateAsync(new UserCredentialDto { }));
        }

        [Fact]
        public async Task AuthenticateAsync_ThrowsException_WhenUserNameIncorrect()
        {
            _userRepository
                .Setup(x => x.GetByUserNameAsync(It.IsAny<string>()))
                .ReturnsAsync(null as User);

            var service = new UserService(_userRepository.Object);
            await Assert.ThrowsAsync<SmartLockApiException>(async () => await service.AuthenticateAsync(new UserCredentialDto { UserName = "abc", Password = "abc" }));
        }

        [Fact]
        public async Task AuthenticateAsync_ThrowsException_WhenPasswordIsIncorrect()
        {
            _userRepository
                .Setup(x => x.GetByUserNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Id = 1, UserName = "abc", Password = "YWJjx" });

            var service = new UserService(_userRepository.Object);
            await Assert.ThrowsAsync<SmartLockApiException>(async () => await service.AuthenticateAsync(new UserCredentialDto { UserName = "abc", Password = "abc" }));
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldLogin_WhenCredentialsAreCorrect()
        {
            _userRepository
                .Setup(x => x.GetByUserNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Id = 1, UserName = "abc", Password = "YWJj" });

            var service = new UserService(_userRepository.Object);
            var result = await service.AuthenticateAsync(new UserCredentialDto { UserName = "abc", Password = "abc" });
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
            Assert.Equal("abc", result.Username);
        }
    }
}
