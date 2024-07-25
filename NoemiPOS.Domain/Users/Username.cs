namespace NoemiPOS.Domain.Users;
public record Username
{
    public string Value { get; }

    public Username(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Username username) => username.Value;
    public static explicit operator Username(string username) => new Username(username);
}
