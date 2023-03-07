using MyReminder.Application.Encryption;
using MyReminder.Application.Messaging;
using MyReminder.Application.Models;
using MyReminder.Domain.Contracts;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Application.User.Features.RevokeToken;

public sealed class RevokeTokenCommandHandler : ICommandHandler<RevokeTokenCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtUtils _jwtUtils;

    public RevokeTokenCommandHandler(
        IUserRepository userRepository,
        IJwtUtils jwtUtils)
    {
        _userRepository = userRepository;
        _jwtUtils = jwtUtils;
    }

    public async Task<Guid> Handle(RevokeTokenCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByRefreshToken(new Token(command.Token));
        var refreshToken = user.RefreshTokens.Single(x => x.Token == new Token(command.Token));

        if (!refreshToken.IsActive() is false)
        {
            // TODO exception
        }

        refreshToken.RevokeToken(new RevokedByIp(command.IpAddress),
            null,
            null);

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _userRepository.CommitAsync(user, cancellationToken);

            return user.Id;
    }
}