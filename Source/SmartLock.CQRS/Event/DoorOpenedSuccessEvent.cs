namespace SmartLock.CQRS.Event
{
    public class DoorOpenedSuccessEvent : ICommand
    {
        public long DoorId { get; set; }
        public long UserId { get; set; }
        public long OfficeId { get; set; }
        public string Comments { get; set; }
    }
}
