using MyReminder.Domain.Common;

namespace MyReminder.Infrastructure.Persistence;

public sealed record Event(
    Guid Id,
    Guid AggregateId,
    string EventName,
    DateTime CreatedAtUtc,
    DateTime SavedAtUtc,
    string JsonContent) : IIdentifiable;
