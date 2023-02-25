using Microsoft.AspNetCore.Identity;
using MyReminder.Application.Messaging;
using MyReminder.Domain.User;

namespace MyReminder.Application.User.Features.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<Domain.User.Entities.User> _userManager;

    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {


        return Guid.NewGuid();
    }
}