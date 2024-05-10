using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Businesses.RegisterBusiness;
public record RegisterBusinessCommand(
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
    string WebSiteUrl) : ICommand<Guid>;
