using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoemiPOS.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NoemiPOS.Infraestructure.Authentication.Services;
public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly string _timeZoneId;

    public JwtService(IOptions<JwtSettings> jwtSettings, IConfiguration configuration)
    {
        _jwtSettings = jwtSettings.Value;
        _timeZoneId = configuration["TimeZone"] ?? "UTC";
    }

    public JwtResponse GenerateToken(Guid userId, string userName, Guid businessId, List<string> roles)
    {
        var securityToken = GenerateSecurityToken(userId, userName, businessId, roles);
        var refreshToken = GenerateRefreshToken();

        return new JwtResponse(
            new JwtSecurityTokenHandler().WriteToken(securityToken),
            refreshToken,
            securityToken.ValidTo);
    }

    private SecurityToken GenerateSecurityToken(Guid userId, string userName, Guid businessId, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            new Claim("UserId", userId.ToString()),
            new Claim("BusinessId", businessId.ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var isBusinessUser = roles.Contains(UserRoles.BusinessAdmin.ToString()) || roles.Contains(UserRoles.BusinessPOS.ToString());
        claims.Add(new Claim("IsBusinessUser", isBusinessUser.ToString()));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddSeconds(15),
            signingCredentials: creds);

        return securityToken;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private DateTime GetExpirtarionLocalDateTimeToken(DateTime tokenExpirationUtc)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(_timeZoneId);
        TimeSpan baseOffset = timeZone.BaseUtcOffset;

        return tokenExpirationUtc + baseOffset;
    }
}


