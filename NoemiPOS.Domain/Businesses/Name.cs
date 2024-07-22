using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Businesses;
public record Name
{
    public string Value { get; }

    public Name(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Name email) => email.Value;
    public static explicit operator Name(string email) => new Name(email);
}
