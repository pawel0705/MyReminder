using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record TokenId(Guid Id) : IdentityValueObject<TokenId>(Id);
