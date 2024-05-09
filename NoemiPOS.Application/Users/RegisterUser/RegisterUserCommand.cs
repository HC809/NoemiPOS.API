using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Application.Users.RegisterUser;
public record RegisterUserCommand(
    Guid BusinessId,
    string FirstName,
    string LastName,
    string Dni,
    string Email,
    string PhoneNumber,
    string Password,
    Role Role,
    string? Username) : ICommand<Guid>;
