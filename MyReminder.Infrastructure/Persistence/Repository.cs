using MyReminder.Domain.Common.Events;
using MyReminder.Domain.Common.ValueObject;
using MyReminder.Domain.Contracts;

namespace MyReminder.Infrastructure.Persistence;

public abstract class Repository<TAggregate, TAggregateId> : IRepository<TAggregate, TAggregateId>
    where TAggregate : AggregateRoot<TAggregateId>
    where TAggregateId : IdentityValueObject<TAggregateId>
{
    protected Repository(DomainEventLogContext dbContext)
    {
        DbContext = dbContext;
    }

    protected DomainEventLogContext DbContext { get; }

    public virtual async Task CreateAsync(TAggregate aggregate, CancellationToken cancellationToken = default)
        => await DbContext.AddAsync(aggregate, cancellationToken);

    public virtual async Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = default)
        => await Task.FromResult(DbContext.Update(aggregate));

    public virtual Task UpdateAsync(ICollection<TAggregate> aggregates, CancellationToken cancellationToken = default)
    {
        DbContext.UpdateRange(aggregates);
        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(TAggregate charger, CancellationToken cancellationToken = default)
        => await Task.FromResult(DbContext.Remove(charger));

    public virtual async Task CommitAsync(TAggregate aggregate, CancellationToken cancellationToken = default)
    {
        var domainEvents = aggregate.DequeueEvents();

        await DbContext.AddEventsAsync(domainEvents, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task CommitAsync(ICollection<TAggregate> aggregates, CancellationToken cancellationToken = default)
    {
        var domainEvents = Array.Empty<DomainEvent>();
        foreach (var aggregate in aggregates)
        {
            domainEvents = domainEvents.Concat(aggregate.DequeueEvents()).ToArray();
        }

        await DbContext.AddEventsAsync(domainEvents, CancellationToken.None);
        await DbContext.SaveChangesAsync(CancellationToken.None);
    }
}
