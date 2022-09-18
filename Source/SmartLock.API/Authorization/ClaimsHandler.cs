using System.Security.Claims;

namespace SmartLock.API.Authorization
{
    public class ClaimsHandler : IClaimsHandler
    {
        public long GetUserId(IEnumerable<Claim> claims)
        {
            var userIdClaim = claims.FirstOrDefault(x => x.Type.Equals("id", StringComparison.OrdinalIgnoreCase));
            if (userIdClaim != null)
            {
                return long.Parse(userIdClaim.Value);
            }
            return 0;
        }

        public long[] GetRoleIds(IEnumerable<Claim> claims)
        {
            var roleIdsClaim = claims.FirstOrDefault(x => x.Type.Equals("roles", StringComparison.OrdinalIgnoreCase));
            if (roleIdsClaim != null)
            {
                return roleIdsClaim.Value.Split(',').Select(x => long.Parse(x)).ToArray();
            }
            return new long[] {};
        }
    }
}
