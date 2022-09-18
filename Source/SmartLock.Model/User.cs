namespace SmartLock.Model
{
    public class User
    {
        public User()
        {
            AuditLogs = new HashSet<AuditLog>();
            UserOfficeRoleMappings = new HashSet<UserOfficeRoleMapping>();
        }

        public long Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public byte IsAdmin { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<UserOfficeRoleMapping> UserOfficeRoleMappings { get; set; }
    }
}