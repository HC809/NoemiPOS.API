using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants;

namespace NoemiPOS.Application.Tenants;
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
        var ownerInfo = new Owner(
            request.OwnerFullName, 
            new Email(request.OwnerEmail),
            new Dni(request.OwnerDni),
            new TenantRtn(request.OwnerRtn) ,
            new PhoneNumber(request.OwnerPhone),
            new SecondaryPhoneNumber(request.OwnerSecondaryPhone));

        var addressInfo = new Address(request.Country, request.State, request.City, request.State, request.PostalCode);

        var tenant = Tenant.Create(new Description(request.Description), ownerInfo, addressInfo, new ManagementNote(request.ManagementNote));

        _tenantRepository.Add(tenant);
        await _unitOfWork.SaveChangesAsync();

        return tenant.Id;
    }
}
