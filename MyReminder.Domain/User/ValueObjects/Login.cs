using MyReminder.Domain.Common.ValueObject;

namespace MyReminder.Domain.User.ValueObjects;

public sealed record Login : ValueObject
{
    public const int MinCharLimit = 6;
    public const int MaxCharLimit = 30;

    public Login(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            // TODO add custom exception
            throw new Exception();
        }

        if (value.Length > MaxCharLimit)
        {
            // TODO add custom exception
            throw new Exception();
        }

        if (value.Length < MinCharLimit)
        {
            // TODO add custom exception
            throw new Exception();
        }

        Value = value;
    }

    public string Value { get; }
}
