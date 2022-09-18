using SmartLock.Model;

namespace SmartLock.Access.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByIdAsync(long id);
    }
}
