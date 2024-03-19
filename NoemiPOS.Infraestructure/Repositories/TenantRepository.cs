using NoemiPOS.Domain.Tenants;

namespace NoemiPOS.Infraestructure.Repositories;
internal sealed class TenantRepository : Repository<Tenant>, ITenantRepository
{
    public TenantRepository(ApplicationDbContext dbContext) : base(dbContext)
    {         
    }
}
