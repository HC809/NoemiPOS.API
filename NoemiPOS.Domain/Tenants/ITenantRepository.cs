namespace NoemiPOS.Domain.Tenants;
public interface ITenantRepository
{
    Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByDniAsync(string dni, CancellationToken cancellationToken = default);
    Task<bool> ExistsByRtnAsync(string rtn, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    void Add(Tenant tenant);
}
