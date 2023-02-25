using MyReminder.Domain.Common.ValueObject;
using MyReminder.Domain.Entities.User.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyReminder.Domain.User.Entities;

public sealed class User : AggregateRoot<UserId>
{
    public User(IdentityValueObject<UserId> id) : base(id)
    {
    }
    public Login Login { get; private set; }

    public Email Email { get; set; }

    public PasswordHash PasswordHash { get; set; }

    public SecurityStamp SecurityStamp { get; set; }

    public bool Verified { get; set; }

    public bool StoreDecrypted { get; set; }

    [NotMapped]
    public bool? Decrypted { get; private set; }

}
