using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Specifications;

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
    
    public async Task<PaginationResult<Order>> GetAllAsync(BaseSpecification<Order> specification, CancellationToken cancellationToken = default)
    {
        var query = context.Orders.AsQueryable();
        if (specification.Criteria != null)
            query = query.Where(specification.Criteria);

        foreach (var include in specification.Includes)
            query = query.Include(include);
        if (specification.OrderBy != null)
            query = query.OrderBy(specification.OrderBy);
        else if (specification.OrderByDescending != null)
            query = query.OrderByDescending(specification.OrderByDescending);

        var totalCount = await query.CountAsync(cancellationToken);


        if (specification.Skip.HasValue && specification.Take.HasValue)
            query = query.Skip(specification.Skip.Value).Take(specification.Take.Value);

        var items = await query.AsNoTracking().ToListAsync(cancellationToken);

        return new PaginationResult<Order>(
            specification.Take ?? totalCount,
            specification.Skip.HasValue ? (specification.Skip.Value / (specification.Take ?? totalCount) + 1) : 1,
            totalCount,
            items);
     
    }
}
