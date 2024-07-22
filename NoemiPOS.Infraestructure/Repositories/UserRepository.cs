using Microsoft.EntityFrameworkCore;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Infraestructure.Repositories;
internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExistsByDniAsync(string dni, CancellationToken cancellationToken = default)
    {
        var dniObjectValue = (Dni)dni;

        return await _dbContext.Set<Tenant>().AnyAsync(x => x.Dni == dniObjectValue, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var emailObjectValue = (Email)email;

        return await _dbContext.Set<User>().AnyAsync(x => x.Email == emailObjectValue, cancellationToken);
    }

    public async Task<User?> GetByEmailOrUsernameAsync(string emailUsername, CancellationToken cancellationToken = default)
    {
        var emailObjectValue = (Email)emailUsername;
        var usernameObjectValue = (Username)emailUsername;

        var user = await _dbContext.Set<User>().
            FirstOrDefaultAsync(x => x.Email == emailObjectValue || x.Username == usernameObjectValue, cancellationToken);

        return user ?? null;
    }
}
