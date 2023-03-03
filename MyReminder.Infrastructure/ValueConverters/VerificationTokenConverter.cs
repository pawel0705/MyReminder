using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class VerificationTokenConverter : ValueConverter<VerificationToken, string>
{
    public VerificationTokenConverter()
        : base(securityStamp => securityStamp.Value, value => new VerificationToken(value))
    {
    }
}
