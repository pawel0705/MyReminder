using MyReminder.Application.Mail;
using MyReminder.Application.Messaging;
using MyReminder.Domain.Contracts;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Application.User.Features.VerifyAccount;

public sealed class VerifyAccountCommandHandler : ICommandHandler<VerifyAccountCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public VerifyAccountCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(VerifyAccountCommand command, CancellationToken cancellationToken)
    {
        await _userRepository.VerifyUser(new VerificationToken(command.Token));

        return Guid.NewGuid();
    }
}