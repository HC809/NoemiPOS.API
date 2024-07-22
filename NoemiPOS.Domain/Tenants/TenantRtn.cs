using NoemiPOS.Domain.Businesses;

namespace NoemiPOS.Domain.Tenants;

public record TenantRtn
{
    public string Value { get; }

    public TenantRtn(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(TenantRtn email) => email.Value;
    public static explicit operator TenantRtn(string email) => new TenantRtn(email);
}
