using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyReminder.Domain.User.Entities;

namespace MyReminder.Infrastructure.User.EntitiesConfiguration;

public sealed class RefreshTokenEntityConfiguration :  IEntityTypeConfiguration<RefreshToken>
{
    private const string TableName = "RefreshTokens";

    public void Configure(EntityTypeBuilder<RefreshToken> entityTypeBuilder)
    {
        entityTypeBuilder
            .ToTable(TableName)
            .HasKey(transition => transition.Id);
    }
}
