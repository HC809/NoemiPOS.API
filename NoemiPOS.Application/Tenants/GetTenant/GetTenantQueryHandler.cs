using NoemiPOS.Application.Abstractions.Data;
using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Domain.Abstractions;
using Dapper;
using NoemiPOS.Domain.Tenants;
using NoemiPOS.Application.Shared;

namespace NoemiPOS.Application.Tenants.GetTenant;
internal sealed class GetTenantQueryHandler : IQueryHandler<GetTenantQuery, TenantResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetTenantQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<TenantResponse>> Handle(GetTenantQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                t.id AS Id,
                t.full_name AS FullName,
                t.email AS Email,
                t.dni AS Dni,
                t.rtn AS Rtn,
                t.phone AS Phone,
                t.secondary_phone AS SecondaryPhone,
                t.description AS Description,
                t.management_note AS ManagementNote,
                t.address_country AS country,
                t.address_state AS state,
                t.address_city AS city,
                t.address_street AS street,
                t.address_postal_code AS postalCode
            FROM tenants AS t
            WHERE id = @TenantId
            """;

        IEnumerable<TenantResponse>? tenants = await connection.QueryAsync<TenantResponse, AddressResponse, TenantResponse>(sql, (tenant, address) =>
        {
            tenant.Address = address;
            return tenant;
        },
        param: new { request.TenantId },
        splitOn: "Country");

        TenantResponse? tenant = tenants.FirstOrDefault();

        if (tenant is null) return Result.Failure<TenantResponse>(TenantErrors.NotFound);

        return Result.Success(tenant);
    }
}
