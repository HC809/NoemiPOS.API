using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NoemiPOS.Application.Authentication.LoginUser;
using NoemiPOS.Infraestructure.Authentication.Services;

namespace NoemiPOS.Api.Controllers.Authentication;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize]
    [HttpGet]
    public IActionResult AuthTest()
    {
        return Ok("Auth test successfully!!");
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Username, request.Password);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }
}
