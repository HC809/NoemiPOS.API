using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Businesses;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Application.Users.RegisterBusinessUser;
internal class RegisterBusinessUserCommandHandler : ICommandHandler<RegisterBusinessUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IBusinessRepository _businessRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordService _passwordService;

    public RegisterBusinessUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordService passwordService, IBusinessRepository businessRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
        _businessRepository = businessRepository;
    }

    public async Task<Result<Guid>> Handle(RegisterBusinessUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByDniAsync(request.Dni))
            return Result.Failure<Guid>(UserErrors.ExistsDni);

        if (await _userRepository.ExistsByEmailAsync(request.Email))
            return Result.Failure<Guid>(UserErrors.ExistsEmail);

        Username username = !string.IsNullOrEmpty(request.Username)
            ? new Username(request.Username)
            : new Username(request.Email); //Crear servicio para extraer el texto antes del @

        if (await _userRepository.ExistsByUsernameAsync(username.Value))
            return Result.Failure<Guid>(UserErrors.ExistsUsername);

        var userRoles = new List<string>();
        foreach (var role in request.Roles)
        {
            var isValidRole = Enum.TryParse<UserRoles>(role, out var validRole);

            if (!isValidRole || (role == UserRoles.NoemiSuperAdmin.ToString()))
            {
                return Result.Failure<Guid>(UserErrors.InvalidRole);
            }
            else
            {
                userRoles.Add(validRole.ToString());
            }
        }

        var user = User.Create(
            new Guid(), //Ficticio
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Email(request.Email),
            username,
            new Dni(request.Dni),
            new PhoneNumber(request.PhoneNumber),
            _passwordService.GetHashPassword(request.Password),
            userRoles);

        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync();

        return user.Id;
    }
}