using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Businesses.GetBusinesses;
public sealed record GetBusinessesQuery() : IQuery<IEnumerable<BusinessResponse>>;
