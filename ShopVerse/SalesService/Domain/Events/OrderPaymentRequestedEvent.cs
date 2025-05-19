namespace Domain.Events;
public sealed record OrderPaymentRequestedEvent(OrderId OrderId, decimal TotalAmount)
    : DomainEventBase
{
}
