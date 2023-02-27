using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record ReplacedByToken : ValueObject
{
    public ReplacedByToken(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
