using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Businesses;
public sealed class Business : BaseEntity
{
    private Business(
        Guid id,
        Guid tenantId,
        Name name,
        Rtn rtn,
        Email email,
        PhoneNumber phone,
        PhoneNumber? secondaryPhone,
        Address address,
        string type,
        string description,
        ManagementNote managementNote) : base(id)
    {
        TenantId = tenantId;
        Name = name;
        Rtn = rtn;
        Email = email;
        Phone = phone;
        SecondaryPhone = secondaryPhone;
        Address = address;
        Type = type;
        Description = description;
        ManagementNote = managementNote;
    }

    public Guid TenantId { get; private set; }
    public Name Name { get; private set; }
    public Rtn Rtn { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public PhoneNumber? SecondaryPhone { get; private set; }
    public Address Address { get; private set; }
    public string Type { get; private set; }
    public string Description { get; private set; }
    public ManagementNote? ManagementNote { get; private set; }
}
