namespace SmartLock.Model
{
    public class Role
    {
        public Role()
        {
            DoorRoleMappings = new HashSet<DoorRoleMapping>();
            UserOfficeRoleMappings = new HashSet<UserOfficeRoleMapping>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<DoorRoleMapping> DoorRoleMappings { get; set; }
        public virtual ICollection<UserOfficeRoleMapping> UserOfficeRoleMappings { get; set; }
    }
}
