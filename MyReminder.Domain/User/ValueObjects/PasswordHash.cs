using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record PasswordHash : ValueObject
{
    public PasswordHash(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
