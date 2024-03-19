namespace NoemiPOS.Application.Tenants.GetTenant;
public sealed class TenantResponse
{
    public Guid Id { get; init; }
    public string Description { get; init; }
    public string OwnerFullName { get; init; }
    public string OwnerEmail { get; init; }
    public string OwnerDni { get; init; }
    public string OwnerRtn { get; init; }
    public string OwnerPhone { get; init; }
    public string OwnerSecondaryPhone { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public string PostalCode { get; init; }
    public string ManagementNote { get; init; }
}
