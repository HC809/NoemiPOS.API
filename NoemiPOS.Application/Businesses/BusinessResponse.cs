using NoemiPOS.Application.Shared;

namespace NoemiPOS.Application.Businesses;
public sealed class BusinessResponse
{
    public Guid Id { get; init; }
    public Guid TenantId { get; init; }
    public string TenantName { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Rtn { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string SecondaryPhone { get; init; } = string.Empty;
    public AddressResponse Address { get; set; } = new AddressResponse(); 
    public string Type { get; init; } = string.Empty;
    public string ManagementNote { get; init; } = string.Empty;
    public string WebSiteUrl { get; init; } = string.Empty;
}

