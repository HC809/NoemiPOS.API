using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Application.Users.RegisterUser;
public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Dni,
    string Email,
    string Username,
    string Password,
    Role Role) : ICommand<Guid>;
