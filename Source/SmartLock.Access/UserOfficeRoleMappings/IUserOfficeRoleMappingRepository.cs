using SmartLock.Model.Dto;

namespace SmartLock.Access.UserOfficeRoleMappings
{
    public interface IUserOfficeRoleMappingRepository
    {
        Task<IEnumerable<OfficeRole>> GetRoleIdsByUserIdAsync(long userId);
    }
}
