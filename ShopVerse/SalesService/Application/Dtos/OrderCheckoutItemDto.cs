namespace Application.Dtos;

public class OrderCheckoutItemDto
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}
