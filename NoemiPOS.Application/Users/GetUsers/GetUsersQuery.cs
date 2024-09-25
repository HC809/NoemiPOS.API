using NoemiPOS.Application.Abstractions.Messaging;

namespace NoemiPOS.Application.Users.GetUsers;
public sealed record GetUsersQuery() : IQuery<IEnumerable<UserResponse>>;
