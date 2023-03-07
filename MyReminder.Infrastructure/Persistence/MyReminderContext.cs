using Microsoft.EntityFrameworkCore;
using MyReminder.Domain.Common.ValueObject;
using MyReminder.Domain.User.Entities;
using MyReminder.Domain.User.ValueObjects;
using MyReminder.Infrastructure.ValueConverters;
using System.Reflection;

namespace MyReminder.Infrastructure.Persistence;

public class MyReminderContext : DomainEventLogContext
{
    public MyReminderContext(DbContextOptions<MyReminderContext> options) : base(options)
    {
    }

    public DbSet<Domain.User.Entities.User> Users => Set<Domain.User.Entities.User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder modelConfigurationBuilder)
    {
        modelConfigurationBuilder
            .Properties<CreatedByIp>()
            .HaveConversion<CreatedByIpConverter>();

        modelConfigurationBuilder
            .Properties<Email>()
            .HaveConversion<EmailConverter>();

        modelConfigurationBuilder
           .Properties<Login>()
           .HaveConversion<LoginConverter>();

        modelConfigurationBuilder
           .Properties<PasswordHash>()
           .HaveConversion<PasswordHashConverter>();

        modelConfigurationBuilder
            .Properties<ReasonRevoked>()
            .HaveConversion<ReasonRevokedConverter>();

        modelConfigurationBuilder
            .Properties<IdentityValueObject<RefreshTokenId>>()
            .HaveConversion<RefreshTokenIdConverter>();

        modelConfigurationBuilder
            .Properties<ReplacedByToken>()
            .HaveConversion<ReplacedByTokenConverter>();

        modelConfigurationBuilder
            .Properties<ResetToken>()
            .HaveConversion<ResetTokenConverter>();

        modelConfigurationBuilder
            .Properties<RevokedByIp>()
            .HaveConversion<RevokedByIpConverter>();

        modelConfigurationBuilder
          .Properties<SecurityStamp>()
          .HaveConversion<SecurityStampConverter>();

        modelConfigurationBuilder
            .Properties<Token>()
            .HaveConversion<TokenConverter>();

        modelConfigurationBuilder
            .Properties<IdentityValueObject<UserId>>()
            .HaveConversion<UserIdConverter>();

        modelConfigurationBuilder
            .Properties<VerificationToken>()
            .HaveConversion<VerificationTokenConverter>();
    }
}
