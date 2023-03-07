using Microsoft.AspNetCore.Mvc;
using MyReminder.API.Authorization;
using MyReminder.Application.Messaging;
using MyReminder.Application.User.Features.LoginUser;
using MyReminder.Application.User.Features.RefreshToken;
using MyReminder.Application.User.Features.RegisterUser;
using MyReminder.Application.User.Features.RevokeToken;
using MyReminder.Application.User.Features.ValidateResetToken;
using MyReminder.Application.User.Features.VerifyAccount;

namespace MyReminder.API.Controllers;

[Authorize]
[ApiController]
public sealed class UserController : Controller
{
    public UserController(ICommandBus commandBus, IQueryBus queryBus)
        : base(commandBus, queryBus)
    {

    }

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
    public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var command = new RefreshTokenCommand(refreshToken, IpAddress());
        var result = await CommandBus.SendAsync(command, cancellationToken);
        SetTokenCookie(result.RefreshToken);
        return Ok(result);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenCommand command, CancellationToken cancellationToken)
    {
        var token = command.Token ?? Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(token))
        {
            return BadRequest();
        }

        if (!Account.OwnsToken(new Domain.User.ValueObjects.Token(token)) && Account.Role != Domain.Contracts.Role.Admin)
        {
            return Unauthorized();
        }

        command = command with { IpAddress = IpAddress() };

        var result = await CommandBus.SendAsync(command, cancellationToken);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("validate-reset-token")]
    public async Task<IActionResult> ValidateResetToken([FromBody] ValidateResetTokenCommand command, CancellationToken cancellationToken)
    {
        await CommandBus.SendAsync(command, cancellationToken);
        return Ok();
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
