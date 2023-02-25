using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.Entities.User.ValueObjects;

public sealed record SecurityStamp : ValueObject
{
    public SecurityStamp(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
