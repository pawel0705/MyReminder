using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.Entities.User.ValueObjects;

public sealed record UserId(Guid Id) : IdentityValueObject<UserId>(Id);
