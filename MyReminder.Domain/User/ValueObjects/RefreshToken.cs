using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

// TODO refresh token jednak jako encja chyba i damy to jako ICollection
public sealed record RefreshToken(
    Token Token,
    DateTime Expires,
    DateTime Created,
    CreatedByIp CreatedByIp,
    DateTime? Revoked,
    RevokedByIp RevokedByIp,
    ReplacedByToken ReplacedByToken,
    ReasonRevoked ReasonRevoked) : ValueObject;
