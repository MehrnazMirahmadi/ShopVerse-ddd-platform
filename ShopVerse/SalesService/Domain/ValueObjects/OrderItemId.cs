namespace Domain.ValueObjects;

public sealed record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid value)
    {
        Value = value;
    }

    public static OrderItemId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("OrderItemId cannot be empty.");

        return new OrderItemId(value);
    }

    public override string ToString() => Value.ToString();

    public static implicit operator Guid(OrderItemId id) => id.Value;

    public static explicit operator OrderItemId(Guid id) => Of(id);
}

