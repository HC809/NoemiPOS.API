namespace NoemiPOS.Domain.Users;
public interface IPasswordService
{
    string GetHashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}
