namespace Domain.Events;

public sealed record OrderCreatedEvent(Order Order)
    : DomainEventBase
{
}


