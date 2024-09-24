using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Authentication.LoginUser;
public record LoginUserCommand(string Username, string Password) : ICommand<LoginUserResponse>;
