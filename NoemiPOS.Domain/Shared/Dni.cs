namespace NoemiPOS.Domain.Shared;
public record Dni
{
    public string Value { get; }

    public Dni(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Dni email) => email.Value;
    public static explicit operator Dni(string email) => new Dni(email);
}

