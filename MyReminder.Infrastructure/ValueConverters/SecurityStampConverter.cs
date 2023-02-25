using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.Entities.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class SecurityStampConverter : ValueConverter<SecurityStamp, string>
{
    public SecurityStampConverter()
        : base(securityStamp => securityStamp.Value, value => new SecurityStamp(value))
    {
    }
}
