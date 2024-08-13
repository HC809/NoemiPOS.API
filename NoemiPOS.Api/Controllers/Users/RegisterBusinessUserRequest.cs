namespace NoemiPOS.Api.Controllers.Users;

public sealed record RegisterBusinessUserRequest(
    string FirstName,
    string LastName,
    string Dni,
    string Email,
    string PhoneNumber,
    string Password,
    List<string> Roles,
    string Username);