using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Users;
public sealed class User : BaseTenantEntity
{
    private User(
        Guid id,
        Guid businessId,
        FirstName firstName,
        LastName lastName,
        Email email,
        Username username,
        Dni dni,
        PhoneNumber phoneNumber,
        string hashPassword
        ) : base(id, businessId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Username = username;
        Dni = dni;
        PhoneNumber = phoneNumber;
        HashPassword = hashPassword;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public Dni Dni { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string HashPassword { get; private set; }

    public static User Create(
        Guid businessId,
        FirstName firstName,
        LastName lastName,
        Email email,
        Dni dni,
        PhoneNumber phoneNumber,
        string hashPassword)
    {
        var username = new Username("test");

        var user = new User(
            Guid.NewGuid(),
            businessId,
            firstName,
            lastName,
            email,
            username,
            dni,
            phoneNumber,
            hashPassword);

        return user;
    }
}
