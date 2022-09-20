using Microsoft.EntityFrameworkCore;
using SmartLock.Model;

namespace SmartLock.Access.DoorRoleMappings
{
    public class DoorRoleMappingRepository : IDoorRoleMappingRepository
    {
        private readonly SmartLockDbContext _context;
        private readonly DbSet<DoorRoleMapping> _dbSet;
        public DoorRoleMappingRepository(SmartLockDbContext context)
        {
            _context = context;
            _dbSet = context.Set<DoorRoleMapping>();
        }

        public async Task<IEnumerable<Door>> GetDoorsByRoleIdsAsync(long[] roleIds, long[] officeIds)
        {
            return await _dbSet.Where(x => roleIds.Contains(x.RoleId) && officeIds.Contains(x.Door.OfficeId)).Select(x => x.Door).ToListAsync();
        }
    }
}
