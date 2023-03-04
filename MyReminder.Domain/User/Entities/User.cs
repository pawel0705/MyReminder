using MyReminder.Domain.Common.ValueObject;
using MyReminder.Domain.Contracts;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Domain.User.Entities;

public sealed class User : AggregateRoot<UserId>
{
    public User(
        IdentityValueObject<UserId> id,
        Login login,
        Email email,
        PasswordHash passwordHash,
        Role role,
        VerificationToken verificationToken) : base(id)
    {
        Login = login;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        VerificationToken = verificationToken;

        Created = DateTime.UtcNow;
    }

    public Login Login { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public Role Role { get; private set; }
    public VerificationToken VerificationToken { get; private set; }
    public DateTime? Verified { get; private set; }
    public ResetToken? ResetToken { get; private set; }
    public DateTime? ResetTokenExpires { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime? Updated { get; private set; }
    public ICollection<RefreshToken> RefreshTokens { get; private set; }

    public bool OwnsToken(Token token)
    {
        return this.RefreshTokens?.FirstOrDefault(x => x.Token == token) is not null;
    }
}
