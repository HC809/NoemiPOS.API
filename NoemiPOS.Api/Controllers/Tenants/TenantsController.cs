using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoemiPOS.Application.Tenants.GetTenant;
using NoemiPOS.Application.Tenants.RegisterTenant;

namespace NoemiPOS.Api.Controllers.Tenants;

[ApiController]
[Route("api/tenants")]
public class TenantsController : ControllerBase
{
    private readonly ISender _sender;

    public TenantsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetTenant(CancellationToken cancellationToken)
    {
        var query = new GetTenantsQuery();
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterTenant(RegisterTenantRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterTenantCommand(
                request.Description,
                request.OwnerFullName,
                request.OwnerEmail,
                request.OwnerDni,
                request.OwnerRtn,
                request.OwnerPhone,
                request.OwnerSecondaryPhone,
                request.Country,
                request.State,
                request.City,
                request.Street,
                request.PostalCode,
                request.ManagementNote);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetTenant), new { id = result.Value }, result.Value);
    }
}
