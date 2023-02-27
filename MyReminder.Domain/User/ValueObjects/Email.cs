using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record Email : ValueObject
{
    public const int MaxCharLimit = 100;

    public Email(string value)
    {
        if (value.Length > MaxCharLimit)
        {
            // TODO
            throw new Exception();
        }

        Value = value;
    }

    public string Value { get; }
}
