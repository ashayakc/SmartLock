using SmartLock.Model;

namespace SmartLock.Access.Doors
{
    public interface IDoorRepository
    {
        Task<Door?> GetByIdAsync(long id);
    }
}
