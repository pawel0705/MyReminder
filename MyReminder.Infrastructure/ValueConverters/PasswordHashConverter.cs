using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class PasswordHashConverter : ValueConverter<PasswordHash, string>
{
    public PasswordHashConverter()
        : base(passwordHash => passwordHash.Value, value => new PasswordHash(value))
    {
    }
}
