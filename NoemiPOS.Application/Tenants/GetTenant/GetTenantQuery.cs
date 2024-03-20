using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Tenants.GetTenant;
public sealed record GetTenantQuery() : IQuery<IEnumerable<TenantResponse>>;
