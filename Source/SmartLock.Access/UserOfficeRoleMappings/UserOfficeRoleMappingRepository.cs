using Microsoft.EntityFrameworkCore;
using SmartLock.Model;
using SmartLock.Model.Dto;

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

        public async Task<IEnumerable<OfficeRole>> GetRoleIdsByUserIdAsync(long userId)
        {
            return await _dbSet.Where(x => x.UserId == userId).Select(x => new OfficeRole { RoleId = x.RoleId, OfficeId = x.OfficeId }).ToListAsync();
        }
    }
}
