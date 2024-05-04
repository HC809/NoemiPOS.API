using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Businesses;
public sealed class Business : Entity
{
    private Business(
        Guid id,
        Guid tenantId,
        Name name,
        Description description,
        BusinessRtn rtn,
        Email email,
        PhoneNumber phone,
        WebSiteUrl? webSiteUrl,
        SecondaryPhoneNumber? secondaryPhone,
        BusinessAddress address,
        BusinessType type,
        ManagementNote managementNote) : base(id)
    {
        TenantId = tenantId;
        Name = name;
        Description = description;
        Rtn = rtn;
        Email = email;
        Phone = phone;
        SecondaryPhone = secondaryPhone;
        WebSiteUrl = webSiteUrl;
        Address = address;
        Type = type;
        ManagementNote = managementNote;
    }
    private Business()
    {
            
    }

    public Guid TenantId { get; private set; }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public BusinessRtn Rtn { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public SecondaryPhoneNumber? SecondaryPhone { get; private set; }
    public WebSiteUrl? WebSiteUrl { get; private set; }
    public BusinessAddress Address { get; private set; }
    public BusinessType Type { get; private set; }
    public ManagementNote ManagementNote { get; private set; }
}
