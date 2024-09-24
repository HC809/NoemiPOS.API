namespace NoemiPOS.Domain.Users;
public interface IJwtService
{
    JwtResponse GenerateToken(Guid userId, string userName, Guid businessId, List<string> roles);
}


public record JwtResponse(string Token, string RefreshToken, DateTime ExpiresIn);