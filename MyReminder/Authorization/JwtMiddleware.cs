using MyReminder.Application.Encryption;
using MyReminder.Domain.Contracts;
using MyReminder.Domain.Entities.User.ValueObjects;

namespace MyReminder.API.Authorization;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateToken(token);
        if (userId != null)
        {
            var userParsedId = Guid.Parse(userId);

            // attach user to context on successful jwt validation
            context.Items["User"] = userRepository.GetById(new UserId(userParsedId));
        }

        await _next(context);
    }
}
