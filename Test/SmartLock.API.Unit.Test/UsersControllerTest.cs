using Moq;
using SmartLock.Controllers;
using SmartLock.DataLogic.Users;
using SmartLock.Model.Dto;

namespace SmartLock.API.Unit.Test
{
    public class UsersControllerTest
    {
        private readonly Mock<IUserService> mockUsersService;
        public UsersControllerTest()
        {
            mockUsersService = new Mock<IUserService>();
        }

        [Fact]
        public async Task AuthenticateAsync_WhenUserTriesToLogin_ShouldLoginSuccessfully()
        {
            mockUsersService
                .Setup(x => x.AuthenticateAsync(It.IsAny<UserCredentialDto>()));

            var action = new UsersController(mockUsersService.Object);
            await action.AuthenticateAsync(new UserCredentialDto { UserName = "abc", Password = "abc" });

            mockUsersService
                .Verify(x => x.AuthenticateAsync(It.IsAny<UserCredentialDto>()), Times.Once);
        }
    }
}