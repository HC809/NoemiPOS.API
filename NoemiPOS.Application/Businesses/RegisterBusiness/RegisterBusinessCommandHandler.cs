using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Businesses;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants;

namespace NoemiPOS.Application.Businesses.RegisterBusiness;
internal class RegisterBusinessCommandHandler : ICommandHandler<RegisterBusinessCommand, Guid>
{
    private readonly IBusinessRepository _businessRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterBusinessCommandHandler(IBusinessRepository businessRepository, ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
    {
        _businessRepository = businessRepository;
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterBusinessCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);

        if (tenant is null)
            return Result.Failure<Guid>(BusinessErrors.TenantNotFound);

        if (await _businessRepository.ExistsByRtnAsync(request.Rtn))
            return Result.Failure<Guid>(BusinessErrors.ExistsRtn);

        if (await _businessRepository.ExistsByEmailAsync(request.Email))
            return Result.Failure<Guid>(BusinessErrors.ExistsEmail);

        if (await _businessRepository.ExistsByNameAsync(request.Name))
            return Result.Failure<Guid>(BusinessErrors.ExistsName);

        if (!Enum.TryParse<BusinessType>(request.Type, true, out var businessType))
            return Result.Failure<Guid>(BusinessErrors.InvalidBusinessType);

        var business = Business.Create(
            tenant.Id,
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
