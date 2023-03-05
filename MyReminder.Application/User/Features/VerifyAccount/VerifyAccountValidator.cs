using FluentValidation;
using MyReminder.Application.User.Features.VerifyAccount;

namespace MyReminder.Application.User.Features.RegisterUser;

public sealed class VerifyAccountValidator : AbstractValidator<VerifyAccountCommand>
{
    public VerifyAccountValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}
