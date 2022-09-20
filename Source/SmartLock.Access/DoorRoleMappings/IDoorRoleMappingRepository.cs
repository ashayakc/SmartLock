using SmartLock.Model;

namespace SmartLock.Access.DoorRoleMappings
{
    public interface IDoorRoleMappingRepository
    {
        Task<IEnumerable<Door>> GetDoorsByRoleIdsAsync(long[] roleIds);
    }
}
