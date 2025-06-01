namespace Domain.ValueObjects;
public sealed record ProductId
{
    public Guid Value { get; }
    private ProductId() { }
    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("OrderItemId cannot be empty.");

        return new ProductId(value);
    }

    public override string ToString() => Value.ToString();

    public static implicit operator Guid(ProductId id) => id.Value;

    public static explicit operator ProductId(Guid id) => Of(id);
}

