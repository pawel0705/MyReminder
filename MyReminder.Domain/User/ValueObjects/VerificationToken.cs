using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record VerificationToken : ValueObject
{
    public VerificationToken(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
