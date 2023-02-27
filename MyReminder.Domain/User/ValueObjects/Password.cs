using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record Password : ValueObject
{
    public Password(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
