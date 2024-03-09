using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Tenants;
public sealed class Tenant : BaseEntity
{
    private Tenant(Guid id, Owner owner, Address adress, Description description, ManagementNote? managementNote) : base(id)
    {
        Owner = owner;
        Address = adress;
        Description = description;
        ManagementNote = managementNote;
    }

    public Owner Owner { get; private set; }
    public Address Address { get; private set; }
    public Description? Description { get; private set; }
    public ManagementNote? ManagementNote { get; private set; }
}
