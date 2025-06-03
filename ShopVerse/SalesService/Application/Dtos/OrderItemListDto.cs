namespace Application.Dtos;

public class OrderItemListDto
{
    public Guid OrderItemId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
