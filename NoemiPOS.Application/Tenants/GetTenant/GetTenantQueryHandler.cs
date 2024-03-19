using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Application.Tenants.GetTenant;
internal sealed class GetTenantQueryHandler : IQueryHandler<GetTenantQuery, TenantResponse>
{
    public Task<Result<TenantResponse>> Handle(GetTenantQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
