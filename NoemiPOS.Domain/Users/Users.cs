using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Users;
public sealed class User : AuditableEntity
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
        string hashPassword,
        List<string> roles
        ) : base(id, businessId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Username = username;
        Dni = dni;
        Phone = phoneNumber;
        HashPassword = hashPassword;
        Roles = roles ?? new List<string>();
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public Dni? Dni { get; private set; }
    public PhoneNumber? Phone { get; private set; }
    public string HashPassword { get; private set; }
    public List<string> Roles { get; private set; }

    public static User Create(Guid businessId, FirstName firstName, LastName lastName, Email email, Username username, Dni dni, PhoneNumber phoneNumber, string hashPassword, List<string> roles)
    {
        var user = new User(
            Guid.NewGuid(),
            businessId,
            firstName,
            lastName,
            email,
            username,
            dni,
            phoneNumber,
            hashPassword,
            roles);

        return user;
    }

#nullable disable
    internal User() { }
#nullable restore
}
