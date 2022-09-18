using System.Security.Claims;

namespace SmartLock.API.Authorization
{
    public interface IClaimsHandler
    {
        long GetUserId(IEnumerable<Claim> claims);
        long[] GetRoleIds(IEnumerable<Claim> claims);
    }
}
