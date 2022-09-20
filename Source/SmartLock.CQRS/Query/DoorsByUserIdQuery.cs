namespace SmartLock.CQRS.Query
{
    public class DoorsByUserIdQuery : IQuery
    {
        public long UserId { get; set; }
    }
}
