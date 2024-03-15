using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Tenants;
public sealed record Owner(
    string FullName,
    Email Email,
    Dni Dni,
    TenantRtn Rtn,
    PhoneNumber Phone,
    SecondaryPhoneNumber SecondaryPhone
    );
