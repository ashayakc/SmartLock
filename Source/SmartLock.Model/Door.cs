namespace SmartLock.Model
{
    public class Door
    {
        public Door()
        {
            AuditLogs = new HashSet<AuditLog>();
            DoorRoleMappings = new HashSet<DoorRoleMapping>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public long OfficeId { get; set; }

        public virtual Office Office { get; set; } = null!;
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<DoorRoleMapping> DoorRoleMappings { get; set; }
    }
}
