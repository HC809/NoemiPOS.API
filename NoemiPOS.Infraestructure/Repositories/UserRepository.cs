using Microsoft.EntityFrameworkCore;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Infraestructure.Repositories;
internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExistsByDniAsync(string dni, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<User>()
         .FromSqlInterpolated($"SELECT * FROM users WHERE dni = {dni}")
         .AnyAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<User>()
         .FromSqlInterpolated($"SELECT * FROM users WHERE email = {email}")
         .AnyAsync();
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
