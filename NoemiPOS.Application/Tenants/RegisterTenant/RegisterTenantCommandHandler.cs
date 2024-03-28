using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants;

namespace NoemiPOS.Application.Tenants.RegisterTenant;
internal sealed class RegisterTenantCommandHandler : ICommandHandler<RegisterTenantCommand, Guid>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
    {
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterTenantCommand request, CancellationToken cancellationToken)
    {
        if (await _tenantRepository.ExistsByDniAsync(request.Dni))
            return Result.Failure<Guid>(TenantErrors.ExistsDni);

        if (await _tenantRepository.ExistsByRtnAsync(request.Rtn))
            return Result.Failure<Guid>(TenantErrors.ExistsRtn);

        if (await _tenantRepository.ExistsByEmailAsync(request.Email))
            return Result.Failure<Guid>(TenantErrors.ExistsEmail);

        var tenant = Tenant.Create(
            new FullName(request.FullName),
            new Email(request.Email),
            new Dni(request.Dni),
            new TenantRtn(request.Rtn),
            new PhoneNumber(request.Phone),
            new SecondaryPhoneNumber(request.SecondaryPhone),
            new TenantAddress(request.Country, request.State, request.City, request.State, request.PostalCode),
            new Description(request.Description),
            new ManagementNote(request.ManagementNote));

        _tenantRepository.Add(tenant);
        await _unitOfWork.SaveChangesAsync();

        return tenant.Id;
    }
}
