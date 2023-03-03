using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class ResetTokenConverter : ValueConverter<ResetToken, string>
{
    public ResetTokenConverter()
        : base(login => login.Value, value => new ResetToken(value))
    {
    }
}
