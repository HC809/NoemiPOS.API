
namespace NoemiPOS.Domain.Businesses;
public interface IBusinessRepository
{
    Task<Business?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByRtnAsync(string rtn, CancellationToken cancellationToken = default);
    void Add(Business tenant);
}
