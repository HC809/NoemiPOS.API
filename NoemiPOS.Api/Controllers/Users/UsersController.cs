using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoemiPOS.Application.Users.GetUsers;
using NoemiPOS.Application.Users.RegisterBusinessUser;
using NoemiPOS.Application.Users.RegisterUser;
using NoemiPOS.Infraestructure.Authorization;

namespace NoemiPOS.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
[Authorize(Policy = PoliciesConstants.NoemiSuperAdminOrBusinessAdminPolicy)]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register-business-user")]
    [Authorize(Policy = PoliciesConstants.BusinessAdminPolicy)]
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

    #region Noemi Super Admin Endpoints
    [HttpGet("all-system-users")]
    [Authorize(Policy = PoliciesConstants.NoemiSuperAdminPolicy)]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery();
        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
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
    #endregion
}
