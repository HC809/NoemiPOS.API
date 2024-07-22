using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Businesses;

public record BusinessRtn
{
    public string Value { get; }

    public BusinessRtn(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(BusinessRtn email) => email.Value;
    public static explicit operator BusinessRtn(string email) => new BusinessRtn(email);
}
