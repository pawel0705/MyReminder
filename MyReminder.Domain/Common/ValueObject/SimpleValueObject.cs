namespace MyReminder.Domain.Common.ValueObject;

public abstract record SimpleValueObject<TValue, TObject>(TValue Value) : ValueObject
    where TValue : IComparable<TValue>
    where TObject : class
{
    public TValue Value { get; } = Value;

    public static implicit operator TValue(SimpleValueObject<TValue, TObject> simpleValueObject)
    {
        return simpleValueObject.Value;
    }

    public sealed override string ToString()
    {
        var value = Value?.ToString();

        return value ?? string.Empty;
    }
}
