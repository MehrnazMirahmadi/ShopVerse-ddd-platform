namespace Application.Dtos;

public class OrderListDto
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public int Status { get; set; }
    public List<OrderItemListDto> OrderItems { get; set; } = new();
}
