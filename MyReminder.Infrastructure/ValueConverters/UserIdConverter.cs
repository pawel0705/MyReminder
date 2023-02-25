using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.Common.ValueObject;
using MyReminder.Domain.Entities.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class UserIdConverter : ValueConverter<IdentityValueObject<UserId>, Guid>
{
    public UserIdConverter()
        : base(userId => userId, value => new UserId(value))
    {
    }
}
