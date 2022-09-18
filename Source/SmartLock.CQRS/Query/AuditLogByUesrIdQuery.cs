namespace SmartLock.CQRS.Query
{
    public class AuditLogByUesrIdQuery : IQuery
    {
        public long UserId { get; set; }
    }
}
