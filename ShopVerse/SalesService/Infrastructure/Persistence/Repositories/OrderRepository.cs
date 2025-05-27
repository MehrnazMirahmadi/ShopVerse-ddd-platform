namespace Infrastructure.Persistence.Repositories;

public class OrderRepository(OrderDbContext context) : IOrderRepository
{

    public async Task AddOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        await context.Orders.AddAsync(order, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await context.Orders.AnyAsync(o => o.Id.Value == orderId, cancellationToken);
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id.Value == orderId, cancellationToken);

        if (order == null)
            throw new KeyNotFoundException("Order not found.");

        return order;
    }

    public async Task UpdateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Update(order);
    }
}
