using NoemiPOS.Domain.Businesses;

namespace NoemiPOS.Infraestructure.Repositories;
internal sealed class BusinessRepository : Repository<Business>, IBusinessRepository
{
    public BusinessRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByRtnAsync(string rtn, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
