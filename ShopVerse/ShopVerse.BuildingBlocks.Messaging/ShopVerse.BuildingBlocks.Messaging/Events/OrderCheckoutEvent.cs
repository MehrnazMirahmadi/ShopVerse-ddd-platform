using ShopVerse.BuildingBlocks.Messaging.Events;

namespace ShopVerse.BuildingBlocks.Messaging.Events;

public record OrderCheckoutEvent : IntegrationEvent
{
    public string OrderName { get; init; } = string.Empty;
    public string ShippingCity { get; init; } = string.Empty;
    public string ShippingStreet { get; init; } = string.Empty;
    public string BillingCity { get; init; } = string.Empty;
    public string BillingStreet { get; init; } = string.Empty;
    public decimal TotalPrice { get; init; }
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public List<OrderCheckoutItem> Items { get; init; } = new();
}
