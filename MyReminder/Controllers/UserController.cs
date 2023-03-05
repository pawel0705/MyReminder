using Microsoft.AspNetCore.Mvc;
using MyReminder.API.Authorization;
using MyReminder.Application.Messaging;
using MyReminder.Application.User.Features.LoginUser;
using MyReminder.Application.User.Features.RegisterUser;
using MyReminder.Application.User.Features.VerifyAccount;

namespace MyReminder.API.Controllers;

[ApiController]
public sealed class UserController : Controller
{
    public UserController(ICommandBus commandBus, IQueryBus queryBus)
        : base(commandBus, queryBus)
    {

    }

    // TODO
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
    {
        command = command with { IpAddress = IpAddress() };
        var response = await CommandBus.SendAsync(command, cancellationToken);
        SetTokenCookie(response.RefreshToken);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        command = command with { Origin = Request.Headers["origin"]! };
        var userResult = await CommandBus.SendAsync(command, cancellationToken);

        return Created(string.Empty, userResult);
    }

    [AllowAnonymous]
    [HttpPost("verify-account")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> VerifyAccount([FromQuery] VerifyAccountCommand command, CancellationToken cancellationToken)
    {
        var result = await CommandBus.SendAsync(command, cancellationToken);

        // todo jakieś info
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = 
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
