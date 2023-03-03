using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyReminder.Application.Messaging;
using MyReminder.Application.User.Features.LoginUser;
using MyReminder.Application.User.Features.RegisterUser;

namespace MyReminder.API.Controllers;

[ApiController]
public sealed class UserController : Controller
{
    public UserController(ICommandBus commandBus, IQueryBus queryBus)
        : base(commandBus, queryBus)
    {

    }

    [HttpPost("login")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
    {
        command = command with { IpAddress = IpAddress() };
        var response = await CommandBus.SendAsync(command, cancellationToken);
        SetTokenCookie(response.RefreshToken);

        return Ok(response);
    }

    [HttpPost("register")]
    [ProducesResponseType(201)]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var userResult = await CommandBus.SendAsync(command, cancellationToken);

        return Created(string.Empty, userResult);
    }

    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }

    private string IpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            return Request.Headers["X-Forwarded-For"]!;
        }
        else
        {
            return HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        }
    }
}
