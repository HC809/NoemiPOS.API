using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Domain.Tenants.Events;
public sealed record TenantRegisteredDomainEvent(Guid TenantId) : IDomainEvent;
