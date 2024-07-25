namespace NoemiPOS.Domain.Users;
public interface IJwtService
{
    string GenerateToken(Guid userId, string userName, Guid businessId);
}
