using FluentValidation;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Application.User.Features.LoginUser;

public sealed class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.Login).MaximumLength(Login.MaxCharLimit);
        RuleFor(x => x.Login).MinimumLength(Login.MinCharLimit);
        RuleFor(x => x.Password).NotEmpty();
    }
}
