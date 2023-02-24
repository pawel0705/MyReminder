namespace MyReminder.Domain.Common.ValueObject;

public abstract record IdentityValueObject<TObject>(Guid Id) : ValueObject, IIdentifiable
{
    public static implicit operator Guid(IdentityValueObject<TObject> identityValueObject)
        => identityValueObject.Id;
}
