using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyReminder.Domain.Entities.User.ValueObjects;

namespace MyReminder.Infrastructure.ValueConverters;

public sealed class LoginConverter : ValueConverter<Login, string>
{
    public LoginConverter()
        : base(login => login.Value, value => new Login(value))
    {
    }
}
