using Dapper;
using NoemiPOS.Application.Abstractions.Data;
using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Application.Shared;
using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Application.Businesses.GetBusinesses;
internal sealed class GetBusinessesQueryHandler : IQueryHandler<GetBusinessesQuery, IEnumerable<BusinessResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetBusinessesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IEnumerable<BusinessResponse>>> Handle(GetBusinessesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                b.id AS Id,
                b.tenant_id AS TenantId,
                b.name AS Name,
                b.description AS Description,
                b.rtn AS Rtn,
                b.email AS Email,   
                b.phone AS Phone,
                b.secondary_phone AS SecondaryPhone,
                b.web_site_url AS WebSiteUrl,
                b.type AS Type,
                b.management_note AS ManagementNote,
                t.full_name as TenantName,
                b.address_country AS country,
                b.address_state AS state,
                b.address_city AS city,
                b.address_street AS street,
                b.address_postal_code AS postalCode
            FROM
                businesses AS b
            join tenants t on b.tenant_id = t.id;
            """;

        IEnumerable<BusinessResponse> businesses = await connection.QueryAsync<BusinessResponse, AddressResponse, BusinessResponse>(sql, (business, address) =>
        {
            business.Address = address;
            return business;
        }, splitOn: "country");

        return Result.Success(businesses);
    }
}
