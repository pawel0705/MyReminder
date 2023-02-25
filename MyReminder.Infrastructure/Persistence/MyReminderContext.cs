using Microsoft.EntityFrameworkCore;
using MyReminder.Domain.Common.ValueObject;
using MyReminder.Domain.Entities.User.ValueObjects;
using MyReminder.Infrastructure.ValueConverters;
using System.Reflection;

namespace MyReminder.Infrastructure.Persistence;

public class MyReminderContext : DbContext
{
    public MyReminderContext(DbContextOptions<MyReminderContext> options) : base(options)
    {
    }

    public DbSet<Domain.User.Entities.User> Users => Set<Domain.User.Entities.User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder modelConfigurationBuilder)
    {
        modelConfigurationBuilder
            .Properties<IdentityValueObject<UserId>>()
            .HaveConversion<UserIdConverter>();

        modelConfigurationBuilder
            .Properties<Login>()
            .HaveConversion<LoginConverter>();

        modelConfigurationBuilder
            .Properties<Email>()
            .HaveConversion<EmailConverter>();

        modelConfigurationBuilder
           .Properties<PasswordHash>()
           .HaveConversion<PasswordHashConverter>();

        modelConfigurationBuilder
          .Properties<SecurityStamp>()
          .HaveConversion<SecurityStampConverter>();
    }
}
