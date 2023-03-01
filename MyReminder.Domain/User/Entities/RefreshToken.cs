using MyReminder.Domain.Common.Entity;
using MyReminder.Domain.Common.ValueObject;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Domain.User.Entities;

public class RefreshToken : Entity<TokenId>
{
    public RefreshToken(IdentityValueObject<TokenId> id) : base(id)
    {
    }

    public Token Token { get; private set; }
    public DateTime Expires { get; private set; }
    public DateTime Created { get; private set; }
    public CreatedByIp CreatedByIp { get; private set; }
    public DateTime? Revoked { get; private set; }
    public RevokedByIp RevokedByIp { get; private set; }
    public ReplacedByToken ReplacedByToken { get; private set; }
    public ReasonRevoked ReasonRevoked { get; private set; }

    public bool IsExpired()
    {
        return DateTime.UtcNow >= Expires;
    }

    public bool IsRevoked()
    {
        return Revoked is not null;
    }

    public bool IsActive()
    {
        return Revoked is null && IsExpired() is false;
    }
}
