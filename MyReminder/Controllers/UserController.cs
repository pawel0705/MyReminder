using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyReminder.Application.Messaging;
using MyReminder.Application.User.Features.RegisterUser;

namespace MyReminder.API.Controllers;

[ApiController]
public sealed class UserController : Controller
{
    public UserController(ICommandBus commandBus, IQueryBus queryBus)
        : base(commandBus, queryBus)
    {

    }

    [HttpPost("register")]
    [ProducesResponseType(201)]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var userResult = await CommandBus.SendAsync(command, cancellationToken);

        return Created(string.Empty, userResult);
    }
}
