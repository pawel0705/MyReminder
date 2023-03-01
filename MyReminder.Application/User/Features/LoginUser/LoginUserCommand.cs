using MyReminder.Application.Messaging;

namespace MyReminder.Application.User.Features.LoginUser;

public sealed record LoginUserCommand(
    string Login,
    string Password) : ICommand<Guid>;
