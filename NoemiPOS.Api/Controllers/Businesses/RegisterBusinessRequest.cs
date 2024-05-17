namespace NoemiPOS.Api.Controllers.Businesses;

public sealed record RegisterBusinessRequest(
    Guid TenantId,
    string Name,
    string Description,
    string Rtn,
    string Email,
    string Phone,
    string SecondaryPhone,
    string Country,
    string State,
    string City,
    string Street,
    string PostalCode,
    string Type,
    string ManagementNote,
    string WebSiteUrl);
