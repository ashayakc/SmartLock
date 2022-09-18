using Microsoft.EntityFrameworkCore;
using SmartLock.Model;

namespace SmartLock.Access.Doors
{
    public class DoorRepository : IDoorRepository
    {
        private readonly SmartLockDbContext _context;
        private readonly DbSet<Door> _dbSet;
        public DoorRepository(SmartLockDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Door>();
        }

        public Task<Door?> GetByIdAsync(long id)
        {
            return _dbSet.Include(x => x.DoorRoleMappings).SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
