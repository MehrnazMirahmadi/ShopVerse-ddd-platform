using Application.Inventory.Commands.DecreaseInventoryItem;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using ShopVerse.BuildingBlocks.Messaging.Events;

namespace Application.Inventory.EventHandlers.Integration;

public class OrderCheckoutEventHandler
    (ISender sender,ILogger<OrderCheckoutEventHandler> logger)
    : IConsumer<OrderCheckoutEvent>
{
    public async Task Consume(ConsumeContext<OrderCheckoutEvent> context)
    {
        
        logger.LogInformation("Integration Event handled:{IntegrationEvent}", context.Message.GetType().Name);
        foreach (var item in context.Message.Items)
        {
            var decreaseCommand = new DecreaseInventoryCommand(item.ProductId, item.Quantity);
            await sender.Send(decreaseCommand);
        }

    }
}
