using Domain.Enums;
using Domain.Events;
namespace Domain.Entities;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    private Order() { }

    internal Order(OrderId id, CustomerId customerId, OrderName orderName,
                   Address shippingAddress, Address billingAddress,
                   Payment payment, OrderStatus status = OrderStatus.Pending)
    {
        Id = id;
        CustomerId = customerId;
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;
    }
    public static Order Create(
    OrderId id, CustomerId customerId, OrderName orderName,
    Address shippingAddress, Address billingAddress,
    Payment payment, OrderStatus status = OrderStatus.Pending)
    {
        var order = new Order(id, customerId, orderName, shippingAddress, billingAddress, payment, status);
        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }
    internal void AddOrderItem(OrderItemId id, ProductId productId, int quantity, Money price)
    {
        var item = OrderItem.Create(id, this.Id, productId, quantity, price);
        _orderItems.Add(item);
    }

    public void SetOrderItems(IEnumerable<OrderItem> items)
    {
        _orderItems.Clear();
        _orderItems.AddRange(items);
    }

    internal void SetStatus(OrderStatus newStatus)
    {
        Status = newStatus;
    }
    public Money TotalPrice =>
    _orderItems.Aggregate(Money.Of(0, "IRR"), (sum, item) => sum + item.Price * item.Quantity);

}

