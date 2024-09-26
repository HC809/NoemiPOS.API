using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Users.GetBusinessUsers;

public sealed record GetBusinessUsersQuery() : IQuery<IEnumerable<UserResponse>>;
