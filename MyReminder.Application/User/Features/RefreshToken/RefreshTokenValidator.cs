using FluentValidation;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Application.User.Features.RefreshToken;

public sealed class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {

    }
}
