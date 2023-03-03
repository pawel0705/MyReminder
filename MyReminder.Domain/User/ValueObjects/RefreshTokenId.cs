using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record RefreshTokenId(Guid Id) : IdentityValueObject<RefreshTokenId>(Id);
