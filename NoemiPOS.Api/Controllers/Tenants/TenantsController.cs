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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTenant(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTenantQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetTenants(CancellationToken cancellationToken)
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
                request.FullName,
                request.Email,
                request.Dni,
                request.Rtn,
                request.Phone,
                request.Phone,
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
