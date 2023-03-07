using Microsoft.Extensions.Logging;
using MyReminder.Domain.Common.Events;
using Newtonsoft.Json;

namespace MyReminder.Infrastructure.Persistence;

public static class EventFactory
{
    public static Event CreateFromDomainEvent(DomainEvent domainEvent)
        => new(
        Guid.Empty,
        domainEvent.AggregateId,
        domainEvent.GetType().FullName ?? nameof(domainEvent),
        domainEvent.CreatedAtUtc,
        DateTime.UtcNow,
        JsonConvert.SerializeObject(domainEvent));
}
