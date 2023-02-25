using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.Common.Entity;

public abstract class Entity<TEntityId> where TEntityId : IdentityValueObject<TEntityId>
{
    protected Entity(IdentityValueObject<TEntityId> id)
    {
        Id = id;
    }

    public IdentityValueObject<TEntityId> Id { get; }
}
