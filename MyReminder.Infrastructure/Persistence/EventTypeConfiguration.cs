using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MyReminder.Infrastructure.Persistence;

public sealed class EventTypeConfiguration : IEntityTypeConfiguration<Event>
{
    private const string TableName = "Events";

    public void Configure(EntityTypeBuilder<Event> entityTypeBuilder)
    {
        entityTypeBuilder
            .ToTable(TableName)
            .HasKey(@event => @event.Id);

        entityTypeBuilder
            .Property(nameof(Event.AggregateId))
            .HasColumnName(nameof(Event.AggregateId))
            .IsRequired();

        entityTypeBuilder
            .Property(nameof(Event.EventName))
            .HasColumnName(nameof(Event.EventName))
            .HasMaxLength(500)
            .IsRequired();

        entityTypeBuilder
            .Property(nameof(Event.CreatedAtUtc))
            .HasColumnName(nameof(Event.CreatedAtUtc))
            .HasColumnType("datetime")
            .IsRequired();

        entityTypeBuilder
            .Property(nameof(Event.SavedAtUtc))
            .HasColumnName(nameof(Event.SavedAtUtc))
            .HasColumnType("datetime")
            .IsRequired();

        entityTypeBuilder
            .Property(nameof(Event.JsonContent))
            .HasColumnName(nameof(Event.JsonContent))
            .IsRequired();
    }
}
