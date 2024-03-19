using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants.Events;

namespace NoemiPOS.Domain.Tenants;
public sealed class Tenant : BaseEntity
{
    private Tenant(Guid id, Owner owner, Address adress, Description description, ManagementNote managementNote) : base(id)
    {
        Owner = owner;
        Address = adress;
        Description = description;
        ManagementNote = managementNote;
    }

    public Owner Owner { get; private set; }
    public Address Address { get; private set; }
    public Description Description { get; private set; }
    public ManagementNote ManagementNote { get; private set; }

    public static Tenant Create(Owner owner, Address adress, Description description, ManagementNote managementNote) {
        var tenant = new Tenant(Guid.NewGuid(), owner, adress, description, managementNote);
        tenant.RaiseDomainEvent(new TenantRegisteredDomainEvent(tenant.Id));

        return tenant;
    }
}
