using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class RevokedByIpConverter : ValueConverter<RevokedByIp, string>
{
    public RevokedByIpConverter()
        : base(login => login.Value, value => new RevokedByIp(value))
    {
    }
}
