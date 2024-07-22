using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Authentication.LoginUser;
public record LoginUserCommand(string username, string password) : ICommand<LoginUserResponse>;
