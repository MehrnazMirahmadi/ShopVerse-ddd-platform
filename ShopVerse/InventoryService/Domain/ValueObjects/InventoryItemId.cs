namespace Domain.ValueObjects;

public sealed record InventoryItemId
{
    public Guid Value { get; }
    
    private InventoryItemId(Guid value)
    {
        Value = value;
    }

    public static InventoryItemId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("InventoryItemId cannot be empty.");

        return new InventoryItemId(value);
    }

    public static InventoryItemId New() => new(Guid.NewGuid());

    public static implicit operator Guid(InventoryItemId id) => id.Value;
    public static explicit operator InventoryItemId(Guid id) => Of(id);

    public override string ToString() => Value.ToString();
}