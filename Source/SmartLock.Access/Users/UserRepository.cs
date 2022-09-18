using Microsoft.EntityFrameworkCore;
using SmartLock.Model;

namespace SmartLock.Access.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly SmartLockDbContext _context;
        private readonly DbSet<User> _dbSet;
        public UserRepository(SmartLockDbContext context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public Task<User?> GetByUserNameAsync(string userName)
        {
            return _dbSet.Include(x => x.UserOfficeRoleMappings).SingleOrDefaultAsync(x => x.UserName == userName);
        }

        public Task<User?> GetByIdAsync(long id)
        {
            return _dbSet.Include(x => x.UserOfficeRoleMappings).SingleOrDefaultAsync(_ => _.Id == id);
        }
    }
}
