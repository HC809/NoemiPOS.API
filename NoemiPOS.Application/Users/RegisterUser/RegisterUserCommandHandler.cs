using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordService _passwordService;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByDniAsync(request.Dni))
            return Result.Failure<Guid>(UserErrors.ExistsDni);

        if (await _userRepository.ExistsByEmailAsync(request.Email))
            return Result.Failure<Guid>(UserErrors.ExistsEmail);

        var user = User.Create(
            request.BusinessId,
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Email(request.Email),
            !string.IsNullOrEmpty(request.Username) ? new Username(request.Username) : new Username(request.Email),
            new Dni(request.Email),
            new PhoneNumber(request.PhoneNumber),
            _passwordService.GetHashPassword(request.Password));

        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync();

        return user.Id;
    }
}
