using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SmartLock.API.Authorization;
using SmartLock.Controllers;
using SmartLock.Service.Doors;
using System.Security.Claims;

namespace SmartLock.API.Unit.Test
{
    public class DoorsControllerTest
    {
        private readonly Mock<IDoorService> mockDoorService;
        private readonly Mock<IClaimsHandler> mockClaimsHandler;
        public DoorsControllerTest()
        {
            mockDoorService = new Mock<IDoorService>();
            mockClaimsHandler = new Mock<IClaimsHandler>();
        }

        [Fact]
        public async Task OpenDoorAsync_WhenRequestedForDoorOpening_ShouldOpenTheDoorSuccessfully()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", "1"),
                new Claim("roles", "1"),
                new Claim("isadmin", "1"),
            }, "mock"));
            mockDoorService
                 .Setup(x => x.OpenDoorAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long[]>(), It.IsAny<string>()));
            mockClaimsHandler
                .Setup(x => x.GetUserId(It.IsAny<IEnumerable<Claim>>()))
                .Returns(It.IsAny<long>());
            mockClaimsHandler
                .Setup(x => x.GetRoleIds(It.IsAny<IEnumerable<Claim>>()))
                .Returns(It.IsAny<long[]>());

            var controller = new DoorsController(mockDoorService.Object, mockClaimsHandler.Object, new NullLoggerFactory());
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            await controller.OpenAsync(1, "NA");

            mockDoorService
                .Verify(x => x.OpenDoorAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long[]>(), It.IsAny<string>()), Times.Once);
        }
    }
}