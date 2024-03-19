using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Tenants.GetTenant;
public sealed record GetTenantQuery(Guid TenantId) : IQuery<TenantResponse>;
