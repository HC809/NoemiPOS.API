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
        SecondaryPhoneNumber? secondaryPhone,
        BusinessAddress address,
        BusinessType type,
        ManagementNote managementNote,
        WebSiteUrl? webSiteUrl) : base(id)
    {
        TenantId = tenantId;
        Name = name;
        Description = description;
        Rtn = rtn;
        Email = email;
        Phone = phone;
        SecondaryPhone = secondaryPhone;
        Address = address;
        Type = type;
        ManagementNote = managementNote;
        WebSiteUrl = webSiteUrl;
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

    public static Business Create(
        Guid tenantId,
        Name name,
        Description description,
        BusinessRtn rtn,
        Email email,
        PhoneNumber phoneNumber,
        SecondaryPhoneNumber secondaryPhoneNumber,
        BusinessAddress address,
        BusinessType type,
        ManagementNote managementNote,
        WebSiteUrl? webSiteUrl)
    {
        var user = new Business(
            Guid.NewGuid(),
            tenantId,
            name,
            description,
            rtn,
            email,
            phoneNumber,
            secondaryPhoneNumber,
            address,
            type,
            managementNote,
            webSiteUrl ?? null);

        return user;
    }
}
