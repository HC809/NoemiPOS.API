namespace NoemiPOS.Api.Controllers.Tenants;

public sealed record RegisterTenantRequest(
    string Description,
    string FullName,
    string Email,
    string Dni,
    string Rtn,
    string Phone,
    string SecondaryPhone,
    string Country,
    string State,
    string City,
    string Street,
    string PostalCode,
    string ManagementNote);
