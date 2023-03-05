using MyReminder.Application.Messaging;
using MyReminder.Application.Models;

namespace MyReminder.Application.User.Features.LoginUser;

public sealed record LoginUserCommand(
    string Login,
    string Password,
    string IpAddress) : ICommand<AuthenticateResponse>;
