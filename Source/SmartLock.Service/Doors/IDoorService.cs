using SmartLock.Model.Dto;

namespace SmartLock.Service.Doors
{
    public interface IDoorService
    {
        Task OpenDoorAsync(long doorId, long userId, long[] roleIds, string comments);
        Task<IEnumerable<DoorDto>> GetByUserIdAsync(long userId);
    }
}
