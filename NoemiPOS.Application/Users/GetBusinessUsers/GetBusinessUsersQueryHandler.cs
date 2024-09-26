using Dapper;
using NoemiPOS.Application.Abstractions.Data;
using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Application.Abstractions.Multitenancy;
using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Application.Users.GetBusinessUsers;

internal sealed class GetBusinessUsersQueryHandler : IQueryHandler<GetBusinessUsersQuery, IEnumerable<UserResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ICurrentUserService _currentUserService;

    public GetBusinessUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory, ICurrentUserService currentUserService)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _currentUserService = currentUserService;
    }

    public async Task<Result<IEnumerable<UserResponse>>> Handle(GetBusinessUsersQuery request, CancellationToken cancellationToken)
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
            	u.business_id as businessId
            from users u
            where u.business_id = @BusinessId;
            """;

        var parameters = new DynamicParameters();

        if (_currentUserService.IsBusinessUser)
        {
            parameters.Add("@BusinessId", _currentUserService.BusinessId);
        }

        IEnumerable<UserResponse> users = await connection.QueryAsync<UserResponse>(sql, parameters);

        return Result.Success(users);
    }
}
