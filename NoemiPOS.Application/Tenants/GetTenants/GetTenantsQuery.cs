using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Tenants.GetTenant;
public sealed record GetTenantsQuery() : IQuery<IEnumerable<TenantResponse>>;
