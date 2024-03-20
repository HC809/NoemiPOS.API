namespace NoemiPOS.Domain.Abstractions;

public abstract class BaseTenantEntity : Entity
{
    protected BaseTenantEntity(Guid id, Guid businessId) : base(id)
    {
        BusinessId = businessId;
    }

    public Guid BusinessId { get; init; }
}
