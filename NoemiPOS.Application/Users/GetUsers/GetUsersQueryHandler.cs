using Dapper;
using NoemiPOS.Application.Abstractions.Data;
using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using System.Reflection.Metadata.Ecma335;

namespace NoemiPOS.Application.Users.GetUsers;
internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IEnumerable<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
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
            join businesses b on b.id = u.business_id;
            """;

        IEnumerable<UserResponse> users = await connection.QueryAsync<UserResponse>(sql);

        return Result.Success(users);
    }
}
