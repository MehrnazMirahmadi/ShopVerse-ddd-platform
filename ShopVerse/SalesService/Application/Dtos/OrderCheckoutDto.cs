namespace Application.Dtos;

public class OrderCheckoutDto
{
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public string OrderName { get; init; } = string.Empty;

    public AddressDto ShippingAddress { get; init; }
    public AddressDto BillingAddress { get; init; }

    public PaymentDto Payment { get; init; }

    public List<OrderCheckoutItemDto> Items { get; init; } = new();
}
