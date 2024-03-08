using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Domain.Tenants;
public sealed class Tenant : BaseEntity
{
    private Tenant(
        Guid id,
        Owner owner,
        Address adress,
        string? note) : base(id)
    {
        Owner = owner;
        Address = adress;
        Note = note;
    }

    private Tenant() { }

    public Owner Owner { get; private set; }
    public Address Address { get; private set; }
    public string? Note { get; private set; }
}
