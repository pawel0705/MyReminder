using FluentValidation;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Application.User.Features.RevokeToken;

public sealed class RevokeTokenValidator : AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenValidator()
    {

    }
}
