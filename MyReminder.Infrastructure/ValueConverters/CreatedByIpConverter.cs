using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class CreatedByIpConverter : ValueConverter<CreatedByIp, string>
{
    public CreatedByIpConverter()
        : base(email => email.Value, value => new CreatedByIp(value))
    {
    }
}
