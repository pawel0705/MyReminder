using MyReminder.Domain.Common.ValueObject;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Domain.User.Entities;

public sealed class User : AggregateRoot<UserId>
{
    public User(
        IdentityValueObject<UserId> id,
        Login login,
        Email email,
        PasswordHash passwordHash) : base(id)
    {
        Login = login;
        Email = email;
        PasswordHash = passwordHash;
    }

    public Login Login { get; private set; }

    public Email Email { get; set; }

    public PasswordHash PasswordHash { get; set; }
}
