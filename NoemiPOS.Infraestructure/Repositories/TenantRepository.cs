using Microsoft.EntityFrameworkCore;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Infraestructure.Repositories;
internal sealed class TenantRepository : Repository<Tenant>, ITenantRepository
{
    public TenantRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExistsByDniAsync(string dni, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Tenant>()
         .FromSqlInterpolated($"SELECT * FROM tenants WHERE dni = {dni}")
         .AnyAsync();
    }

    public async Task<bool> ExistsByRtnAsync(string rtn, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Tenant>()
         .FromSqlInterpolated($"SELECT * FROM tenants WHERE rtn = {rtn}")
         .AnyAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var emailObjectValue = (Email)email;

        return await _dbContext.Set<Tenant>().AnyAsync(x => x.Email == emailObjectValue, cancellationToken);
    }
}
