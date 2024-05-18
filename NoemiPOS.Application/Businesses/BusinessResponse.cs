using NoemiPOS.Application.Shared;

namespace NoemiPOS.Application.Businesses;
public sealed class BusinessResponse
{
    public Guid Id { get; init; }
    public Guid TenantId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Rtn { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public string SecondaryPhone { get; init; }
    public AddressResponse Address { get; set; }
    public string Type { get; init; }
    public string ManagementNote { get; init; }
    public string WebSiteUrl { get; init; }
}
