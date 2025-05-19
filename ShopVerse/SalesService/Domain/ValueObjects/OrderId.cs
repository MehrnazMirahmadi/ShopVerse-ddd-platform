namespace Domain.ValueObjects;

public sealed record OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("OrderId cannot be empty.");

        return new OrderId(value);
    }

    public override string ToString() => Value.ToString();

    public static implicit operator Guid(OrderId id) => id.Value;

    public static explicit operator OrderId(Guid id) => Of(id);
}
