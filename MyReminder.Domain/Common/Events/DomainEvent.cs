using MediatR;

namespace MyReminder.Domain.Common.Events;

public abstract record DomainEvent : IIdentifiable, INotification
{
    protected DomainEvent(
        Guid aggregateId,
        Guid id = default,
        DateTime createdAtUtc = default,
        bool isPublished = false)
    {
        AggregateId = aggregateId;

        Id = id == default
            ? Guid.NewGuid()
            : id;

        CreatedAtUtc = createdAtUtc == default
            ? DateTime.UtcNow
            : DateTime.SpecifyKind(createdAtUtc, DateTimeKind.Utc);

        IsPublished = isPublished;
    }

    public Guid Id { get; }
    public Guid AggregateId { get; }
    public DateTime CreatedAtUtc { get; }
    public bool IsPublished { get; }
}
