using NoemiPOS.Domain.Users;

namespace NoemiPOS.Domain.Tenants;
public interface ITenantRepository
{
    Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Tenant tenant);
}
