using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Domain.Categories;
internal sealed class Category : AuditableEntity
{
    private Category(Guid id, Guid businessId) : base(id, businessId)
    {
    }


#nullable disable
    internal Category() { }
#nullable restore
}
