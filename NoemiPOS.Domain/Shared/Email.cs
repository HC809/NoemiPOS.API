namespace NoemiPOS.Domain.Shared;
public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;
    public static explicit operator Email(string email) => new Email(email);
}
