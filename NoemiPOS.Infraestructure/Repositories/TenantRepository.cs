using Microsoft.EntityFrameworkCore;
using NoemiPOS.Domain.Tenants;

namespace NoemiPOS.Infraestructure.Repositories;
internal sealed class TenantRepository : Repository<Tenant>, ITenantRepository
{
    public TenantRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExistsByDniAsync(string dni, CancellationToken cancellationToken = default)
    {
        bool exists = await _dbContext.Set<Tenant>()
         .FromSqlInterpolated($"SELECT * FROM Tenants WHERE Dni = {dni}")
         .AnyAsync();

        return exists;
    }
}
