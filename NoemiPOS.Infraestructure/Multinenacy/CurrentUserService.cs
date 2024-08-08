using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace NoemiPOS.Infraestructure.Multinenacy;
internal sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            return userIdClaim is not null ? Guid.Parse(userIdClaim) : Guid.Empty;
        }
    }

    public Guid BusinessId
    {
        get
        {
            var businessIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("BusinessId")?.Value;
            return businessIdClaim is not null ? Guid.Parse(businessIdClaim) : Guid.Empty;
        }
    }
}
