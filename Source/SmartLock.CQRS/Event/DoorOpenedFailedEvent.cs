namespace SmartLock.CQRS.Event
{
    public class DoorOpenedFailedEvent : ICommand
    {
        public long DoorId { get; set; }
        public long UserId { get; set; }
        public long OfficeId { get; set; }
        public string Comments { get; set; }
    }
}
