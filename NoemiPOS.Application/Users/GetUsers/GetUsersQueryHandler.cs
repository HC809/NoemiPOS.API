using Dapper;
using NoemiPOS.Application.Abstractions.Data;
using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Application.Abstractions.Multitenancy;
using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Application.Users.GetUsers;
internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ICurrentUserService _currentUserService;

    public GetUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory, ICurrentUserService currentUserService)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _currentUserService = currentUserService;
    }

    public async Task<Result<IEnumerable<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            select 
            	u.id,
            	u.first_name as FirstName,
            	u.last_name as LastName,
            	u.email,
            	u.username,
            	u.dni, 
            	u.phone, 
            	array_to_string(u.roles, ',') as roles, 
            	b.id as businessId,
            	b."name" as businessName
            from users u
            join businesses b on b.id = u.business_id;
            """;

        IEnumerable<UserResponse> users = await connection.QueryAsync<UserResponse>(sql);

        return Result.Success(users);
    }
}
