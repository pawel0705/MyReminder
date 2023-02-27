using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record ReasonRevoked : ValueObject
{
    public ReasonRevoked(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
