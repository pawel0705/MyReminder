using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyReminder.Domain.Common.Events;

namespace MyReminder.Infrastructure.Persistence;

public abstract class DomainEventLogContext : DbContext
{
    private readonly string _databaseSchemaName;

    protected DomainEventLogContext(DbContextOptions options, string databaseSchema = "DomainEventLog")
        : base(options)
    {
        _databaseSchemaName = databaseSchema;
    }

    public async Task AddEventsAsync(IEnumerable<DomainEvent> domainEvents, CancellationToken cancellationToken)
    {
    //    var events = domainEvents.Select(EventFactory.CreateFromDomainEvent);
    //    await Set<Event>().AddRangeAsync(events, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
  //      modelBuilder.HasDefaultSchema(_databaseSchemaName);
  //      modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
    }
}
