using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class TokenConverter : ValueConverter<Token, string>
{
    public TokenConverter()
        : base(securityStamp => securityStamp.Value, value => new Token(value))
    {
    }
}
