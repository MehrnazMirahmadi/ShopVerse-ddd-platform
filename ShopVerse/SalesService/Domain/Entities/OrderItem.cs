namespace Domain.Entities;

public class OrderItem : Entity<OrderItemId>
{
    private OrderItem() { } 

    public OrderItemId Id { get; private set; } = default!;
    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public Money Price { get; private set; } = default!;

    internal OrderItem(OrderItemId id, OrderId orderId, ProductId productId, int quantity, Money price)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
    public static OrderItem Create(OrderItemId id, OrderId orderId, ProductId productId, int quantity, Money price)
    {
        return new OrderItem(id, orderId, productId, quantity, price);
    }
}
