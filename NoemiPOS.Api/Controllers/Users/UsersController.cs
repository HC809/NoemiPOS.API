using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoemiPOS.Application.Users.RegisterBusinessUser;
using NoemiPOS.Application.Users.RegisterUser;
using NoemiPOS.Infraestructure.Authorization;

namespace NoemiPOS.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register-admin-user")]
    [Authorize(Policy = PoliciesConstants.NoemiSuperAdminPolicy)]
    public async Task<IActionResult> RegisterAdminUser(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.BusinessId,
            request.FirstName,
            request.LastName,
            request.Dni,
            request.Email,
            request.PhoneNumber,
            request.Password,
            request.Roles,
            request.Username);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("register-business-user")]
    [Authorize(Policy = PoliciesConstants.BusinessAdminPolicy)]
    [HttpPost]
    public async Task<IActionResult> RegisterBusinessUser(RegisterBusinessUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterBusinessUserCommand(
            request.FirstName,
            request.LastName,
            request.Dni,
            request.Email,
            request.PhoneNumber,
            request.Password,
            request.Roles,
            request.Username);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
