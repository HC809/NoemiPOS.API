using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Users;
public record Username
{
    public string Value { get; }

    public Username(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Username email) => email.Value;
    public static explicit operator Username(string email) => new Username(email);
}
