namespace ShopVerse.BuildingBlocks.Messaging.Events;

public class OrderCheckoutEvent
{
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public List<OrderCheckoutItem> Items { get; init; } = new();
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}