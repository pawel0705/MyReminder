using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.Common.ValueObject;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class RefreshTokenIdConverter : ValueConverter<IdentityValueObject<RefreshTokenId>, Guid>
{
    public RefreshTokenIdConverter()
        : base(id => id, value => new RefreshTokenId(value))
    {
    }
}
