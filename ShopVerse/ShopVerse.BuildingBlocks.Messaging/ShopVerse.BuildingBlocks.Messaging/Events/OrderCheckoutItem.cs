namespace ShopVerse.BuildingBlocks.Messaging.Events;

public class OrderCheckoutItem
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
}
