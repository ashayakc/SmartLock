using SmartLock.Access.DoorRoleMappings;
using SmartLock.Access.UserOfficeRoleMappings;
using SmartLock.CQRS.Query;
using SmartLock.CQRS.QueryResult;
using SmartLock.Model;

namespace SmartLock.CQRS.QueryHandler
{
    public class DoorsByUserIdQueryHandler : IQueryHandler<DoorsByUserIdQuery, DoorsByUserIdQueryResult>
    {
        private readonly IUserOfficeRoleMappingRepository _userOfficeRoleMappingRepository;
        private readonly IDoorRoleMappingRepository _doorRoleMappingRepository;
        public DoorsByUserIdQueryHandler(IUserOfficeRoleMappingRepository userOfficeRoleMapping, IDoorRoleMappingRepository doorRoleMappingRepository)
        {
            _userOfficeRoleMappingRepository = userOfficeRoleMapping;
            _doorRoleMappingRepository = doorRoleMappingRepository;
        }

        public async Task<DoorsByUserIdQueryResult> RetrieveAsync(DoorsByUserIdQuery query)
        {
            var roleIds = await _userOfficeRoleMappingRepository.GetRoleIdsByUserIdAsync(query.UserId);
            if(!roleIds.Any())
            {
                return new DoorsByUserIdQueryResult(new List<Door>());
            }

            var doors = await _doorRoleMappingRepository.GetDoorsByRoleIdsAsync(roleIds.ToArray());
            if (!doors.Any())
            {
                return new DoorsByUserIdQueryResult(new List<Door>());
            }
            return new DoorsByUserIdQueryResult(doors);
        }
    }
}
