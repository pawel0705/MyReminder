using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class ReplacedByTokenConverter : ValueConverter<ReplacedByToken, string>
{
    public ReplacedByTokenConverter()
        : base(passwordHash => passwordHash.Value, value => new ReplacedByToken(value))
    {
    }
}
