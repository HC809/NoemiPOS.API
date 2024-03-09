using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Domain.Users;
public sealed class Users : BaseTenantEntity
{
    private Users(Guid id, Guid businessId) : base(id, businessId)
    {

    }
}
