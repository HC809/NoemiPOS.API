using NoemiPOS.Application.Shared;

namespace NoemiPOS.Application.Tenants.GetTenant;
public sealed class TenantResponse
{
    public Guid Id { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Dni { get; init; }
    public string Rtn { get; init; }
    public string Phone { get; init; }
    public string SecondaryPhone { get; init; }
    public AddressResponse Address { get; set; }
    public string Description { get; init; }
    public string ManagementNote { get; init; }
}
