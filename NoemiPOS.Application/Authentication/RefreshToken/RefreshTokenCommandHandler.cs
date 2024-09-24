using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Application.Authentication.RefreshToken;
internal sealed class RefreshTokenCommandHandler //: ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public RefreshTokenCommandHandler(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    //public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    //{
    //    var refreshToken = await _userRepository.GetRefreshTokenAsync(request.RefreshToken);

    //    if (refreshToken == null || refreshToken.IsExpired)
    //    {
    //        return Result.Failure<RefreshTokenResponse>("Invalid or expired refresh token.");
    //    }

    //    var user = await _userRepository.GetByIdAsync(refreshToken.UserId);
    //    if (user == null)
    //    {
    //        return Result.Failure<RefreshTokenResponse>("User not found.");
    //    }

    //    var newTokenResponse = _jwtService.GenerateToken(user.Id, user.Username, user.BusinessId, user.Roles);

    //    return Result.Success(new RefreshTokenResponse(newTokenResponse.Token, newTokenResponse.ExpiresIn));
    //}
}

