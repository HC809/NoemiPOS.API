using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Authentication.RefreshToken;
public record RefreshTokenCommand(string RefreshToken) : ICommand<RefreshTokenResponse>;
