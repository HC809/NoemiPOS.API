using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoemiPOS.Application.Users.RegisterUser;

namespace NoemiPOS.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.BusinessId,
            request.FirstName,
            request.LastName,
            request.Dni,
            request.Email,
            request.PhoneNumber,
            request.Password,
            request.Role,
            request.Username);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
