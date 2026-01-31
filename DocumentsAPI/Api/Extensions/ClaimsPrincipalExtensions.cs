using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DocumentsAPI.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)
                  ?? user.FindFirst(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userIdClaim?.Value))
                throw new UnauthorizedAccessException("UserId not found in token");

            return Guid.Parse(userIdClaim.Value);
        }
    }
}
