using Microsoft.Extensions.Options;
using MyReminder.Application.Encryption;
using MyReminder.Application.Helpers;
using MyReminder.Application.Messaging;
using MyReminder.Application.Models;
using MyReminder.Domain.Contracts;
using MyReminder.Domain.User.ValueObjects;
using System.Security.Principal;

namespace MyReminder.Application.User.Features.RefreshToken;

public sealed class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, AuthenticateResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtUtils _jwtUtils;
    private readonly Settings _settings;

    public RefreshTokenCommandHandler(
        IUserRepository userRepository,
        IJwtUtils jwtUtils,
        IOptions<Settings> settings)
    {
        _userRepository = userRepository;
        _jwtUtils = jwtUtils;
        _settings = settings.Value;
    }

    public async Task<AuthenticateResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByRefreshToken(new Token(command.RefreshToken));

        var refreshToken = user.RefreshTokens.Single(x => x.Token == new Token(command.RefreshToken));

        if (refreshToken.IsRevoked())
        {
            // revoke all descendant tokens in case this token has been compromised
            RevokeDescendantRefreshTokens(
                refreshToken,
                user,
                command.IpAddress,
                $"Attempted reuse of revoked ancestor token: {command.RefreshToken}");

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _userRepository.CommitAsync(user, cancellationToken);
        }

        if (!refreshToken.IsActive())
        {
            // TODO exception
        }

        // replace old refresh token with a new one (rotate token)
        var newRefreshToken = RotateRefreshToken(refreshToken, command.IpAddress);
        user.RefreshTokens.Add(newRefreshToken);

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _userRepository.CommitAsync(user, cancellationToken);

        // generate new jwt
        var jwtToken = _jwtUtils.GenerateJwtToken(user);

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

    private void RemoveOldRefreshTokens(Domain.User.Entities.User account)
    {
        account.RefreshTokens.ToList().RemoveAll(x =>
            !x.IsActive() &&
            x.Created.AddDays(_settings.RefreshTokenTimeToLive) <= DateTime.UtcNow);
    }

    private Domain.User.Entities.RefreshToken RotateRefreshToken(Domain.User.Entities.RefreshToken refreshToken, string ipAddress)
    {
        var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
        RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token.Value);
        return newRefreshToken;
    }

    private void RevokeDescendantRefreshTokens(Domain.User.Entities.RefreshToken refreshToken, Domain.User.Entities.User user, string ipAddress, string reason)
    {
        // recursively traverse the refresh token chain and ensure all descendants are revoked
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken.Value))
        {
            var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
            if (childToken.IsActive())
            {
                RevokeRefreshToken(childToken, ipAddress, reason);
            }
            else
            {
                RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }

        }
    }

    private void RevokeRefreshToken(Domain.User.Entities.RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
    {
        token.RevokeToken(new RevokedByIp(ipAddress),
            reason != null ? new ReasonRevoked(reason) :null,
            replacedByToken != null ? new ReplacedByToken(replacedByToken) : null);
    }
}
