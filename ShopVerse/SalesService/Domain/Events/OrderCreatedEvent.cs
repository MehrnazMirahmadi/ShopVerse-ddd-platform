namespace Domain.Events;

public sealed record OrderCreatedEvent(OrderId orderId, CustomerId customerId)
    : DomainEventBase
{
}


