using Dapper;
using NoemiPOS.Application.Abstractions.Data;
using NoemiPOS.Application.Abstractions.Messaging;
using NoemiPOS.Application.Shared;
using NoemiPOS.Domain.Abstractions;
using NoemiPOS.Domain.Businesses;

namespace NoemiPOS.Application.Businesses.GetBusiness;
internal sealed class GetBusinessQueryHandler : IQueryHandler<GetBusinessQuery, BusinessResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetBusinessQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<BusinessResponse>> Handle(GetBusinessQuery request, CancellationToken cancellationToken)
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
                b.address_country AS country,
                b.address_state AS state,
                b.address_city AS city,
                b.address_street AS street,
                b.address_postal_code AS postalCode
            FROM businesses AS b
            WHERE id = @BusinessId;
            """;

        IEnumerable<BusinessResponse>? businesses = await connection.QueryAsync<BusinessResponse, AddressResponse, BusinessResponse>(sql, (business, address) =>
        {
            business.Address = address;
            return business;
        }, 
        param: new { request.BusinessId },
        splitOn: "Country");

        BusinessResponse? business = businesses.FirstOrDefault();

        if (business is null) return Result.Failure<BusinessResponse>(BusinessErrors.NotFound);

        return Result.Success(business);
    }
}
