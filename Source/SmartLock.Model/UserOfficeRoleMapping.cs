namespace SmartLock.Model
{
    public class UserOfficeRoleMapping
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long OfficeId { get; set; }
        public long RoleId { get; set; }

        public virtual Office Office { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
