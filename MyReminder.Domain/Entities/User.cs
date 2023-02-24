using MyReminder.Domain.Common.ValueObject;
using MyReminder.Domain.ValueObjects;

namespace MyReminder.Domain.Entities;

public sealed class User : AggregateRoot<UserId>
{
    public User(IdentityValueObject<UserId> id) : base(id)
    {
    }
}
