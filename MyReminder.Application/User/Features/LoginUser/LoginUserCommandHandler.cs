using MyReminder.Application.Encryption;
using MyReminder.Application.Messaging;
using MyReminder.Application.Models;
using MyReminder.Application.User.Features.LoginUser;
using MyReminder.Domain.Contracts;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Application.User.Features.RegisterUser;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AuthenticateResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtUtils _jwtUtils;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IJwtUtils jwtUtils)
    {
        _userRepository = userRepository;
        _jwtUtils = jwtUtils;
    }

    public async Task<AuthenticateResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByLogin(new Login(command.Login));
        if (user.Verified is null || BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash.Value) is false)
        {
            // TODO exception or something bad
        }

        var jwtToken = _jwtUtils.GenerateJwtToken(user);
        var refreshToken = _jwtUtils.GenerateRefreshToken(command.IpAddress);

        await _userRepository.UpdateRefreshTokens(new UserId(user.Id), refreshToken);

        var response = new AuthenticateResponse
        {
            Id = user.Id.Id,
            RefreshToken = refreshToken.Token.Value,
            JwtToken = jwtToken,
            Login = user.Login.Value,
            Role = user.Role,
            Email = user.Email.Value
        };

        return response;
    }
}