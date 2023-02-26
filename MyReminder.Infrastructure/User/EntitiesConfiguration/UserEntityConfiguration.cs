using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyReminder.Domain.Entities.User.ValueObjects;

namespace MyReminder.Infrastructure.User.EntitiesConfiguration;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<Domain.User.Entities.User>
{
    private const string TableName = "Users";

    public void Configure(EntityTypeBuilder<Domain.User.Entities.User> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable(TableName)
            .Ignore(area => area.Events)
            .HasKey(area => area.Id);

        entityTypeBuilder
            .Property(user => user.Login)
            .HasMaxLength(Login.MaxCharLimit)
            .IsRequired();

        entityTypeBuilder
            .Property(user => user.Email)
            .HasMaxLength(Email.MaxCharLimit)
            .IsRequired();

        entityTypeBuilder
            .Property(user => user.PasswordHash)
            .IsRequired();
    }
}
