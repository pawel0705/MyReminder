using MyReminder.Application.Messaging;

namespace MyReminder.Application.User.Features.RegisterUser;

public sealed record RegisterUserCommand(
    string Login,
    string Email,
    string Password,
    string Origin) : ICommand<Guid>;
