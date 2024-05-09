namespace NoemiPOS.Domain.Users;
public interface IPasswordService
{
    public string GetHashPassword(string password);
}
