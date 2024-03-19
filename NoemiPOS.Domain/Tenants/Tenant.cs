using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants.Events;

namespace NoemiPOS.Domain.Tenants;
public sealed class Tenant : BaseEntity
{
    private Tenant(Guid id, Description description, Owner owner, Address adress, ManagementNote managementNote) : base(id)
    {
        Description = description;
        Owner = owner;
        Address = adress;
        ManagementNote = managementNote;
    }

    public Description Description { get; private set; }
    public Owner Owner { get; private set; }
    public Address Address { get; private set; }
    public ManagementNote ManagementNote { get; private set; }

    public static Tenant Create(Description description, Owner owner, Address adress, ManagementNote managementNote)
    {
        var tenant = new Tenant(Guid.NewGuid(), description, owner, adress, managementNote);
        tenant.RaiseDomainEvent(new TenantRegisteredDomainEvent(tenant.Id));

        return tenant;
    }
}
