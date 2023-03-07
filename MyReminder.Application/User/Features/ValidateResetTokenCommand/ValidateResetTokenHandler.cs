using MyReminder.Application.Encryption;
using MyReminder.Application.Messaging;
using MyReminder.Domain.Contracts;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Application.User.Features.ValidateResetToken;

public sealed class ValidateResetTokenCommandHandler : ICommandHandler<ValidateResetTokenCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtUtils _jwtUtils;

    public ValidateResetTokenCommandHandler(
        IUserRepository userRepository,
        IJwtUtils jwtUtils)
    {
        _userRepository = userRepository;
        _jwtUtils = jwtUtils;
    }

    public async Task<Guid> Handle(ValidateResetTokenCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByResetToken(new ResetToken(command.Token));

        if (user is null)
        {
            // TODO throw exception
        }
        // tutaj w sumie tylko sprawdzenie jest czy znajdzie usera po refesh tokenie
        return Guid.NewGuid();
    }
}