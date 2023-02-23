namespace MyReminder.Domain.Common.Events;

public interface IDomainEventPublisher
{
    Task PublishAsync(CancellationToken cancellationToken, params DomainEvent[] domainEvents);
}
