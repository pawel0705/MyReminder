using MyReminder.Domain.Common.Entity;
using MyReminder.Domain.Common.ValueObject;

public abstract class AggregateRoot<TAggregateId> : Entity<TAggregateId>
    where TAggregateId : IdentityValueObject<TAggregateId>
{
    [NonSerialized]
    private readonly Queue<DomainEvent> _domainEvents;

    protected AggregateRoot(IdentityValueObject<TAggregateId> id)
        : base(id)
    {
        _domainEvents = new Queue<DomainEvent>();
    }

    public int Version { get; private set; }

    public DomainEvent[] Events
        => _domainEvents.ToArray();

    public DomainEvent[] DequeueEvents()
    {
        var events = _domainEvents.ToArray();
        _domainEvents.Clear();

        return events;
    }

    protected void Enqueue(DomainEvent domainEvent)
    {
        _domainEvents.Enqueue(domainEvent);
        Version++;
    }
}
