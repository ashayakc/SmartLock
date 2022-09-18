namespace SmartLock.Model
{
    public class DoorRoleMapping
    {
        public long Id { get; set; }
        public long DoorId { get; set; }
        public long RoleId { get; set; }

        public virtual Door Door { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
