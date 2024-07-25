using Microsoft.AspNetCore.Http;

namespace NoemiPOS.Infraestructure.Multinenacy;
internal sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
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
