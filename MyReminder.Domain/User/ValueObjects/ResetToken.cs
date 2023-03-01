using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record ResetToken : ValueObject
{
    public ResetToken(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
