using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record Token : ValueObject
{
    public Token(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
