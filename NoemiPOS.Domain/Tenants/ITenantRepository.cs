using NoemiPOS.Domain.Users;

namespace NoemiPOS.Domain.Tenants;
public interface ITenantRepository
{
    Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByDniAsync(string dni, CancellationToken cancellationToken = default);
    //Task<Tenant?> FindByRtnAsync(string rtn, CancellationToken cancellationToken = default);
    //Task<Tenant?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    void Add(Tenant tenant);
}
