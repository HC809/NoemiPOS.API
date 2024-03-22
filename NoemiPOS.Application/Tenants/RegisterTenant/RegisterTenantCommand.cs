using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Tenants.RegisterTenant;
public record RegisterTenantCommand(
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
    string ManagementNote) : ICommand<Guid>;
