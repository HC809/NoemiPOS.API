using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoemiPOS.Application.Businesses.GetBusiness;
using NoemiPOS.Application.Businesses.GetBusinesses;
using NoemiPOS.Application.Businesses.RegisterBusiness;
using NoemiPOS.Infraestructure.Authorization;

namespace NoemiPOS.Api.Controllers.Businesses;

[ApiController]
[Route("api/businesses")]
[Authorize(Policy = PoliciesConstants.NoemiSuperAdminPolicy)]
public class BusinessesController : ControllerBase
{
    private readonly ISender _sender;

    public BusinessesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBusiness(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBusinessQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetBusinesses(CancellationToken cancellationToken)
    {
        var query = new GetBusinessesQuery();
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterBusiness(RegisterBusinessRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterBusinessCommand(
                request.TenantId,
                request.Name,
                request.Description,
                request.Rtn,
                request.Email,
                request.Phone,
                request.SecondaryPhone,
                request.Country,
                request.State,
                request.City,
                request.Street,
                request.PostalCode,
                request.Type,
                request.ManagementNote,
                request.WebSiteUrl);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetBusiness), new { id = result.Value }, result.Value);
    }
}
