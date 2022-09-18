namespace SmartLock.Model
{
    public class Office
    {
        public Office()
        {
            AuditLogs = new HashSet<AuditLog>();
            Doors = new HashSet<Door>();
            UserOfficeRoleMappings = new HashSet<UserOfficeRoleMapping>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<Door> Doors { get; set; }
        public virtual ICollection<UserOfficeRoleMapping> UserOfficeRoleMappings { get; set; }
    }
}
