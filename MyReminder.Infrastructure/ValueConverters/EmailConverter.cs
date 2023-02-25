using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.Entities.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class EmailConverter : ValueConverter<Email, string>
{
    public EmailConverter()
        : base(email => email.Value, value => new Email(value))
    {
    }
}
