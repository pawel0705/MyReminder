using MyReminder.Application.Messaging;
using MyReminder.Application.User.Features.LoginUser;

namespace MyReminder.Application.User.Features.RegisterUser;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, Guid>
{

    public LoginUserCommandHandler()
    {

    }

    public async Task<Guid> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {


        return Guid.NewGuid();
    }
}