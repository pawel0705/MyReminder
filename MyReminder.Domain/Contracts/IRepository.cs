using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.Contracts;

public interface IRepository<TAggregate, in TAggregateId>
    where TAggregate : AggregateRoot<TAggregateId>
    where TAggregateId : IdentityValueObject<TAggregateId>
{
    Task CreateAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    Task UpdateAsync(ICollection<TAggregate> aggregates, CancellationToken cancellationToken = default);
    Task DeleteAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    Task CommitAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
    Task CommitAsync(ICollection<TAggregate> aggregates, CancellationToken cancellationToken = default);
}
