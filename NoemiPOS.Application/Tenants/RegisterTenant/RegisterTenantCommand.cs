using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Tenants.RegisterTenant;
public record RegisterTenantCommand(
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
    string ManagementNote) : ICommand<Guid>;
