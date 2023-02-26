using Microsoft.AspNetCore.Identity;
using MyReminder.Application.Messaging;
using MyReminder.Domain.Contracts;

namespace MyReminder.Application.User.Features.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{

    public RegisterUserCommandHandler()
    {

    }

    public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {


        return Guid.NewGuid();
    }
}