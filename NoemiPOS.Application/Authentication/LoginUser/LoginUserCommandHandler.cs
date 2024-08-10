using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Application.Authentication.LoginUser;
internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(IUserRepository userRepository, IPasswordService passwordService, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailOrUsernameAsync(request.username);

        if (user == null)
            return Result.Failure<LoginUserResponse>(UserErrors.InvalidCredentials);

        if (!_passwordService.VerifyPassword(request.password, user.HashPassword))
            return Result.Failure<LoginUserResponse>(UserErrors.InvalidCredentials);

        var token = _jwtService.GenerateToken(user.Id, user.Username, user.BusinessId, user.Roles);

        var response = new LoginUserResponse(user.Email, $"{user.FirstName.Value} {user.LastName.Value}", token);

        return response;
    }
}
