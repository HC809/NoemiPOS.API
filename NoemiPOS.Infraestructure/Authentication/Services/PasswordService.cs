using NoemiPOS.Domain.Users;
using BC = BCrypt.Net.BCrypt;

namespace NoemiPOS.Infraestructure.Authentication.Services;

internal sealed class PasswordService : IPasswordService
{
    public string GetHashPassword(string password) => BC.HashPassword(password);

    public bool VerifyPassword(string password, string hashedPassword) => BC.Verify(password, hashedPassword);
}
