namespace NoemiPOS.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailOrUsernameAsync(string emailUsername, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByDniAsync(string dni, CancellationToken cancellationToken = default);
    void Add(User tenant);
}
