using NoemiPOS.Domain.Users;

namespace NoemiPOS.Infraestructure.Authentication.Services;

internal sealed class PasswordService : IPasswordService
{
    public string GetHashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);
}
