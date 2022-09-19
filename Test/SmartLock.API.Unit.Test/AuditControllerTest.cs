using Moq;
using SmartLock.Controllers;
using SmartLock.Model.Dto;
using SmartLock.Service.AuditLogs;

namespace SmartLock.API.Unit.Test
{
    public class AuditControllerTest
    {
        private readonly Mock<IAuditLogService> mockAuditService;
        public AuditControllerTest()
        {
            mockAuditService = new Mock<IAuditLogService>();
        }

        [Fact]
        public async Task GetByUserIdAsync_WhenRequestedForAuditLog_ShouldReturnAuditLogsOfTheUser()
        {
            mockAuditService
                .Setup(x => x.GetAuditLogsAsync(It.IsAny<long>()))
                .ReturnsAsync(new List<AuditLogDto>
                {
                    new AuditLogDto
                    {
                        UserId = 1,
                        Comments = "NA",
                        DoorId = 1,
                        EventTime = DateTime.Now,
                        OfficeId = 1,
                        Status = 1
                    },
                    new AuditLogDto
                    {
                        UserId = 1,
                        Comments = "NA",
                        DoorId = 2,
                        EventTime = DateTime.Now,
                        OfficeId = 1,
                        Status = 0
                    }
                });

            var action = new AuditController(mockAuditService.Object);
            var result = await action.GetByUserIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}