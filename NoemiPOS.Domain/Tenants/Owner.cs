using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Tenants;
public sealed record Owner(
    string FullName,
    Email Email,
    Dni Dni,
    PhoneNumber Phone,
    PhoneNumber SecondaryPhone
    );
