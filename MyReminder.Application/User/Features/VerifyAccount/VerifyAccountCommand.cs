using MyReminder.Application.Messaging;

namespace MyReminder.Application.User.Features.VerifyAccount;

public sealed record VerifyAccountCommand(string Token) : ICommand<Guid>;
