using Domain.Entities;

namespace Domain.Contract.Repositories;

public interface IOrderRepository
{
    Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task AddOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken = default);
}
