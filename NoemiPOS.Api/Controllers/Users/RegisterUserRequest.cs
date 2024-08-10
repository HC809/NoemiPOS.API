using NoemiPOS.Domain.Users;

namespace NoemiPOS.Api.Controllers.Users;

public sealed record RegisterUserRequest(
    Guid BusinessId,
    string FirstName,
    string LastName,
    string Dni,
    string Email,
    string PhoneNumber,
    string Password,
    List<string> Roles,
    string Username);