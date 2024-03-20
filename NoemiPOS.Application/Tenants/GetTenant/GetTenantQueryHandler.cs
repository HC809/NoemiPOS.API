using NoemiPOS.Application.Abstractions.Data;
using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using Dapper;

namespace NoemiPOS.Application.Tenants.GetTenant;
internal sealed class GetTenantQueryHandler : IQueryHandler<GetTenantQuery, IEnumerable<TenantResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetTenantQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IEnumerable<TenantResponse>>> Handle(GetTenantQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                id,
                full_name,
                email,
                dni,
                rtn,
                phone,
                secondary_phone,
                address_country,
                address_state,
                address_city,
                address_street,
                address_postal_code,
                description,
                management_note
            FROM
                tenants;
            """;

        IEnumerable<TenantResponse> tenants = await connection.QueryAsync<TenantResponse>(sql, cancellationToken);

        return Result.Success(tenants);
    }
}
