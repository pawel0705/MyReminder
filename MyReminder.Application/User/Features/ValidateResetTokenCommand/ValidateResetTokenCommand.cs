using MyReminder.Application.Messaging;

namespace MyReminder.Application.User.Features.ValidateResetToken;

public sealed record ValidateResetTokenCommand(string Token) : ICommand<Guid>;
