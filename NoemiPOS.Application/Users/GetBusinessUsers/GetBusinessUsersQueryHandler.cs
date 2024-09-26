using Dapper;
using NoemiPOS.Application.Abstractions.Data;
using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Application.Abstractions.Multitenancy;
using NoemiPOS.Application.Users.GetUsers;
using NoemiPOS.Domain.Abstractions;
using System.Text;

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
            	u.business_id, 
            	u.first_name,
            	u.last_name,
            	u.email,
            	u.username,
            	u.dni, 
            	u.phone, 
            	array_to_string(u.roles, ',') as roles, 
            	b.id as businessId,
            	b."name" as businessName
            from users u
            join businesses b on b.id = u.business_id
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
