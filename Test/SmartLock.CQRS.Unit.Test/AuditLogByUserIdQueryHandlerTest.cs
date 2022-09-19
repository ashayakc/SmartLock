using Moq;
using SmartLock.Access.AuditLogs;
using SmartLock.CQRS.Query;
using SmartLock.CQRS.QueryHandler;
using SmartLock.Model;

namespace SmartLock.CQRS.Unit.Test
{
    public class AuditLogByUserIdQueryHandlerTest
    {
        private readonly Mock<IAuditLogRepository> _auditLogRepository;
        public AuditLogByUserIdQueryHandlerTest()
        {
            _auditLogRepository = new Mock<IAuditLogRepository>();
        }

        [Fact]
        public async Task RetrieveAsync_ShouldReturnEmpty_WhenAuditLogNotPresentForGivenUser()
        {
            _auditLogRepository
                .Setup(x => x.GetByUserIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new List<AuditLog>());

            var handler = new AuditLogByUserIdQueryHandler(_auditLogRepository.Object);
            var result = await handler.RetrieveAsync(new AuditLogByUesrIdQuery { UserId = 1 });
            Assert.NotNull(result);
            Assert.Empty(result.AuditLogs);
        }

        [Fact]
        public async Task RetrieveAsync_ShouldReturnAuditLogs_WhenAuditLogsPresentForGivenUser()
        {
            _auditLogRepository
                .Setup(x => x.GetByUserIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new List<AuditLog>
                {
                    new AuditLog
                    {
                        UserId = 1,
                        Comments = "NA",
                        DoorId = 1,
                        EventTime = DateTime.Now,
                        OfficeId = 1,
                        Status = 1
                    },
                    new AuditLog
                    {
                        UserId = 1,
                        Comments = "NA",
                        DoorId = 2,
                        EventTime = DateTime.Now,
                        OfficeId = 1,
                        Status = 0
                    }
                });

            var handler = new AuditLogByUserIdQueryHandler(_auditLogRepository.Object);
            var result = await handler.RetrieveAsync(new AuditLogByUesrIdQuery { UserId = 1 });
            Assert.NotNull(result);
            Assert.Equal(2, result.AuditLogs.Count());
        }
    }
}
