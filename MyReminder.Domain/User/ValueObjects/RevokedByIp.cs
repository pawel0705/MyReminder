using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record RevokedByIp : ValueObject
{
    public RevokedByIp(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
