using Domain.Enums;
using Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Abstractions;
namespace Domain.Entities;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; }
    public OrderName OrderName { get; private set; }
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    public Payment Payment { get; private set; }
    public OrderStatus Status { get; private set; }

    private Order() { } 

    public Order(OrderId id, CustomerId customerId, OrderName orderName,
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
}
