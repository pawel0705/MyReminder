using MyReminder.Application.Messaging;

namespace MyReminder.Application.User.Features.RevokeToken;

public sealed record RevokeTokenCommand(string Token, string IpAddress) : ICommand<Guid>;
