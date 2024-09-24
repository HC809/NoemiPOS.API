namespace NoemiPOS.Application.Authentication.RefreshToken;

public sealed record RefreshTokenResponse(string Token, DateTime ExpiresIn);
