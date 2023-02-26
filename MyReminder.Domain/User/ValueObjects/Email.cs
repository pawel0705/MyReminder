using MyReminder.Domain.Common.ValueObject;
using System.ComponentModel.DataAnnotations;

namespace MyReminder.Domain.Entities.User.ValueObjects;

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
