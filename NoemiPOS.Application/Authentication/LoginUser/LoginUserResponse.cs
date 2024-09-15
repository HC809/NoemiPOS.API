namespace NoemiPOS.Application.Authentication.LoginUser;
public sealed record LoginUserResponse(string Email, string FullName, string role, string Token, DateTime ExpiresIn);
