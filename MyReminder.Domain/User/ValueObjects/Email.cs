using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.Entities.User.ValueObjects;

public sealed record Email : ValueObject
{
    public Email(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
