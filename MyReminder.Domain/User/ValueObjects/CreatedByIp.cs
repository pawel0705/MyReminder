using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record CreatedByIp : ValueObject
{
    public CreatedByIp(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
