using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Tenants;
public sealed class Tenant : BaseEntity
{
    private Tenant(
        Guid id,
        string businessName,
        string description,
        Email emial,
        TaxRegistryNumber taxRegistryNumber,
        PhoneNumber phoneNumber,
        WebSiteUrl? webSiteUrl) : base(id)
    {
        BusinessName = businessName;
        Description = description;
        Email = emial;
        TaxRegistryNumber = taxRegistryNumber;
        PhoneNumber = phoneNumber;
        WebSiteUrl = webSiteUrl;
    }

    public TaxRegistryNumber TaxRegistryNumber { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public WebSiteUrl? WebSiteUrl { get; private set; }

    public string BusinessName { get; private set; }
    public string Description { get; private set; }
}
