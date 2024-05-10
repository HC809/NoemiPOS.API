using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Businesses;
using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Application.Businesses.RegisterBusiness;
internal class RegisterBusinessCommandHandler : ICommandHandler<RegisterBusinessCommand, Guid>
{
    private readonly IBusinessRepository _businessRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterBusinessCommandHandler(IBusinessRepository businessRepository, IUnitOfWork unitOfWork)
    {
        _businessRepository = businessRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterBusinessCommand request, CancellationToken cancellationToken)
    {
        if (await _businessRepository.ExistsByRtnAsync(request.Rtn))
            return Result.Failure<Guid>(BusinessErrors.ExistsRtn);

        if (await _businessRepository.ExistsByEmailAsync(request.Email))
            return Result.Failure<Guid>(BusinessErrors.ExistsEmail);

        if (await _businessRepository.ExistsByNameAsync(request.Name))
            return Result.Failure<Guid>(BusinessErrors.ExistsName);

        if (!Enum.TryParse<BusinessType>(request.Type, true, out var businessType))
            return Result.Failure<Guid>(BusinessErrors.InvalidBusinessType);

        var business = Business.Create(
            request.TenantId,
            new Name(request.Name),
            new Description(request.Description),
            new BusinessRtn(request.Rtn),
            new Email(request.Email),
            new PhoneNumber(request.Phone),
            new SecondaryPhoneNumber(request.SecondaryPhone),
            new BusinessAddress(request.Country, request.State, request.City, request.State, request.PostalCode),
            businessType,
            new ManagementNote(request.ManagementNote),
            new WebSiteUrl(request.WebSiteUrl));

        _businessRepository.Add(business);
        await _unitOfWork.SaveChangesAsync();

        return business.Id;
    }
}
