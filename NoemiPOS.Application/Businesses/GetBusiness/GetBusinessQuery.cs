using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Businesses.GetBusiness;
public sealed record GetBusinessQuery(Guid BusinessId) : IQuery<BusinessResponse>;
