namespace NoemiPOS.Domain.Tenants;
public record TenantAddress(
    string Country,
    string State,
    string City,
    string? Street,
    string? PostalCode);
