using FluentValidation;
using MyReminder.Domain.Entities.User.ValueObjects;

namespace MyReminder.Application.User.Features.RegisterUser;

public sealed class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.Login).MaximumLength(Login.MaxCharLimit);
        RuleFor(x => x.Login).MinimumLength(Login.MinCharLimit);
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
