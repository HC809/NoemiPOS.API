using NoemiPOS.Application.Shared;

namespace NoemiPOS.Application.Tenants;
public sealed class TenantResponse
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Dni { get; init; } = string.Empty;
    public string Rtn { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string SecondaryPhone { get; init; } = string.Empty;
    public AddressResponse Address { get; set; } = new AddressResponse();
    public string Description { get; init; } = string.Empty;
    public string ManagementNote { get; init; } = string.Empty;
}
