using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants.Events;

namespace NoemiPOS.Domain.Tenants;
public sealed class Tenant : Entity
{
    private Tenant(
        Guid id,
        FullName fullName,
        Email email,
        Dni dni,
        TenantRtn rtn,
        PhoneNumber phoneNumber,
        SecondaryPhoneNumber secondaryPhoneNumber,
        TenantAddress adress,
        Description description,
        ManagementNote managementNote) : base(id)
    {
        FullName = fullName;
        Email = email;
        Dni = dni;
        Rtn = rtn;
        Phone = phoneNumber;
        SecondaryPhone = secondaryPhoneNumber;
        Address = adress;
        Description = description;
        ManagementNote = managementNote;
    }

    private Tenant() { }

    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Dni Dni { get; private set; }
    public TenantRtn Rtn { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public SecondaryPhoneNumber? SecondaryPhone { get; private set; }
    public Description Description { get; private set; }
    public TenantAddress Address { get; private set; }
    public ManagementNote ManagementNote { get; private set; }

    public static Tenant Create(
        FullName fullName,
        Email email,
        Dni dni,
        TenantRtn rtn,
        PhoneNumber phoneNumber,
        SecondaryPhoneNumber secondaryPhoneNumber,
        TenantAddress adress,
        Description description,
        ManagementNote managementNote)
    {
        var tenant = new Tenant(
            Guid.NewGuid(),
            fullName,
            email,
            dni,
            rtn,
            phoneNumber,
            secondaryPhoneNumber,
            adress,
            description,
            managementNote);

        tenant.RaiseDomainEvent(new TenantRegisteredDomainEvent(tenant.Id));

        return tenant;
    }
}
