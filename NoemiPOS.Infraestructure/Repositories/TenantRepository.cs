using Microsoft.EntityFrameworkCore;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants;
using NoemiPOS.Domain.Users;
using System.Net;

namespace NoemiPOS.Infraestructure.Repositories;
internal sealed class TenantRepository : Repository<Tenant>, ITenantRepository
{
    public TenantRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExistsByDniAsync(string dni, CancellationToken cancellationToken = default)
    {
        var dniObjectValue = (Dni)dni;

        return await _dbContext.Set<Tenant>().AnyAsync(x => x.Dni == dniObjectValue, cancellationToken);
    }

    public async Task<bool> ExistsByRtnAsync(string rtn, CancellationToken cancellationToken = default)
    {
        var rtnObjectValue = (TenantRtn)rtn;

        return await _dbContext.Set<Tenant>().AnyAsync(x => x.Rtn == rtnObjectValue, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var emailObjectValue = (Email)email;

        return await _dbContext.Set<Tenant>().AnyAsync(x => x.Email == emailObjectValue, cancellationToken);
    }
}
