using Domain.Entities;

namespace Domain.Events;

public sealed record OrderCreatedEvent(OrderId orderId, CustomerId customerId, List<OrderItem> OrderItems)
    : DomainEventBase
{
}


