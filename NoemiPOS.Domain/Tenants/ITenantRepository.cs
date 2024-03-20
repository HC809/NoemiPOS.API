using NoemiPOS.Domain.Users;

namespace NoemiPOS.Domain.Tenants;
public interface ITenantRepository
{
    void Add(Tenant tenant);
}
