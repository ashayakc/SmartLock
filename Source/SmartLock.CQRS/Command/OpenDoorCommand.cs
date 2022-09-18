namespace SmartLock.CQRS.Command
{
    public class OpenDoorCommand : ICommand
    {
        public long DoorId { get; set; }
        public long UserId { get; set; }
        public long[] RoleIds { get; set; }
        public string Comments { get; set; }
    }
}
