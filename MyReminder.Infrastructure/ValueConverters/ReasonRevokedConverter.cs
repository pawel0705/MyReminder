using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class ReasonRevokedConverter : ValueConverter<ReasonRevoked, string>
{
    public ReasonRevokedConverter()
        : base(passwordHash => passwordHash.Value, value => new ReasonRevoked(value))
    {
    }
}
