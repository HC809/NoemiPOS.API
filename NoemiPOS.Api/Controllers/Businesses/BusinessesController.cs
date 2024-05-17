using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoemiPOS.Application.Businesses.RegisterBusiness;

namespace NoemiPOS.Api.Controllers.Businesses;

[ApiController]
[Route("api/businesses")]
public class BusinessesController : ControllerBase
{
    private readonly ISender _sender;

    public BusinessesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBusiness(Guid id, CancellationToken cancellationToken)
    {
        return Ok();
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
