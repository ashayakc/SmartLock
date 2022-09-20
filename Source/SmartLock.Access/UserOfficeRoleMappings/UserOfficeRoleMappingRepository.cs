using Microsoft.EntityFrameworkCore;
using SmartLock.Model;

namespace SmartLock.Access.UserOfficeRoleMappings
{
    public class UserOfficeRoleMappingRepository : IUserOfficeRoleMappingRepository
    {
        private readonly SmartLockDbContext _context;
        private readonly DbSet<UserOfficeRoleMapping> _dbSet;
        public UserOfficeRoleMappingRepository(SmartLockDbContext context)
        {
            _context = context;
            _dbSet = context.Set<UserOfficeRoleMapping>();
        }

        public async Task<IEnumerable<long>> GetRoleIdsByUserIdAsync(long userId)
        {
            return await _dbSet.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();
        }
    }
}
