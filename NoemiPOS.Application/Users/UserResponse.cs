namespace NoemiPOS.Application.Users;
public sealed class UserResponse
{
    public Guid Id { get; init; }
    public Guid BusinessId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Dni { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Roles { get; init; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;

}
