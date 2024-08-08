namespace NoemiPOS.Domain.Abstractions;
public abstract class AuditableEntity : BaseTenantEntity
{
    protected AuditableEntity(Guid id, Guid businessId) : base(id, businessId)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = string.Empty;
    }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string CreatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }

    public void SetCreated(string createdBy)
    {
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetUpdated(string updatedBy)
    {
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }
}
