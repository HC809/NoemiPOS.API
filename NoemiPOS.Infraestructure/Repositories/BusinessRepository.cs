using Microsoft.EntityFrameworkCore;
using NoemiPOS.Domain.Businesses;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants;

namespace NoemiPOS.Infraestructure.Repositories;
internal sealed class BusinessRepository : Repository<Business>, IBusinessRepository
{
    public BusinessRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var emailObjectValue = (Email)email;

        return await _dbContext.Set<Business>().AnyAsync(x => x.Email == emailObjectValue, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var nameObjectValue = (Name)name;

        return await _dbContext.Set<Business>().AnyAsync(x => x.Name == nameObjectValue, cancellationToken);
    }

    public async Task<bool> ExistsByRtnAsync(string rtn, CancellationToken cancellationToken = default)
    {
        var rtnObjectValue = (BusinessRtn)rtn;

        return await _dbContext.Set<Tenant>().AnyAsync(x => x.Rtn == rtnObjectValue, cancellationToken);
    }
}
