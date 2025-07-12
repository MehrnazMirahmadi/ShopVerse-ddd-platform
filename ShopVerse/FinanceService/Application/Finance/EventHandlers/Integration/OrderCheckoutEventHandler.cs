using Finance.Application.Finance.Commands.CreateInvoice;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using ShopVerse.BuildingBlocks.Messaging.Events;

namespace Finance.Application.Finance.EventHandlers.Integration;

public class OrderCheckoutEventHandler(
    ISender sender,
    ILogger<OrderCheckoutEventHandler> logger)
    : IConsumer<OrderCheckoutEvent>
{
    public async Task Consume(ConsumeContext<OrderCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        logger.LogWarning("🔥 Received OrderCheckoutEvent for OrderId: {OrderId}", context.Message.OrderId);

        var command = new CreateInvoiceCommand(
            InvoiceNumber: $"INV-{context.Message.OrderId.ToString().Substring(0, 8).ToUpper()}",
            IssuedAt: DateTime.UtcNow,
            RelatedEntityType: "Order",
            RelatedEntityId: context.Message.OrderId,
            Items: context.Message.Items.Select(i => new CreateInvoiceItemModel(
                Description: $"محصول {i.ProductId}",
                UnitPrice: i.Price,
                Quantity: i.Quantity
            )).ToList()
        );

        await sender.Send(command);
    }
}
