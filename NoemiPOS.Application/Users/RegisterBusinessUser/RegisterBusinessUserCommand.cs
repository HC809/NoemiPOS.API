using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Users.RegisterBusinessUser;
public record RegisterBusinessUserCommand(
    string FirstName,
    string LastName,
    string Dni,
    string Email,
    string PhoneNumber,
    string Password,
    List<string> Roles,
    string? Username) : ICommand<Guid>;
