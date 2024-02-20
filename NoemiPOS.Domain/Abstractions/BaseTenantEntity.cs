namespace NoemiPOS.Domain.Abstractions;

public abstract class BaseTenantEntity : BaseEntity
{
    protected BaseTenantEntity(Guid id, Guid tenantId) : base(id)
    {
        TenantId = tenantId;
    }

    public Guid TenantId { get; init; }
}
