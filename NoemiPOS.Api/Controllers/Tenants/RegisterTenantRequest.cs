namespace NoemiPOS.Api.Controllers.Tenants;

public sealed record RegisterTenantRequest(
    string Description,
    string OwnerFullName,
    string OwnerEmail,
    string OwnerDni,
    string OwnerRtn,
    string OwnerPhone,
    string OwnerSecondaryPhone,
    string Country,
    string State,
    string City,
    string Street,
    string PostalCode,
    string ManagementNote);
