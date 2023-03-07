using FluentValidation;

namespace MyReminder.Application.User.Features.ValidateResetToken;

public sealed class ValidateResetTokenValidator : AbstractValidator<ValidateResetTokenCommand>
{
    public ValidateResetTokenValidator()
    {

    }
}
