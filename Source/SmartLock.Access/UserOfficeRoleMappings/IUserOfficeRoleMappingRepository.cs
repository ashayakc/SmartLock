namespace SmartLock.Access.UserOfficeRoleMappings
{
    public interface IUserOfficeRoleMappingRepository
    {
        Task<IEnumerable<long>> GetRoleIdsByUserIdAsync(long userId);
    }
}
