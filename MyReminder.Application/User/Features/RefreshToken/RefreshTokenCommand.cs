using MyReminder.Application.Messaging;
using MyReminder.Application.Models;

namespace MyReminder.Application.User.Features.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken,
    string IpAddress) : ICommand<AuthenticateResponse>;

