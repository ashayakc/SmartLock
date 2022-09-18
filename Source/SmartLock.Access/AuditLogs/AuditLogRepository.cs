using Microsoft.EntityFrameworkCore;
using SmartLock.Model;

namespace SmartLock.Access.AuditLogs
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly SmartLockDbContext _context;
        private readonly DbSet<AuditLog> _dbSet;
        public AuditLogRepository(SmartLockDbContext context)
        {
            _context = context;
            _dbSet = context.Set<AuditLog>();
        }

        public async Task<IEnumerable<AuditLog>> GetByUserIdAsync(long userId)
        {
            return await _dbSet.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task AddAsync(AuditLog auditLog)
        {
            await _dbSet.AddAsync(auditLog);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
