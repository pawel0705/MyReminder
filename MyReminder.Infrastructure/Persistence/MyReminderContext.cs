using Microsoft.EntityFrameworkCore;
using MyReminder.Domain.Entities;
using MyReminder.Domain.ValueObjects;
using MyReminder.Infrastructure.ValueConverters;
using System.Reflection;

namespace MyReminder.Infrastructure.Persistence;

public class MyReminderContext : DbContext
{
    public MyReminderContext(DbContextOptions<MyReminderContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder modelConfigurationBuilder)
    {
        modelConfigurationBuilder
            .Properties<UserId>()
            .HaveConversion<UserIdConverter>();
    }
}
