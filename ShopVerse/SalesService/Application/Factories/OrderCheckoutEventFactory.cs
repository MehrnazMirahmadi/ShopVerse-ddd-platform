using Domain.Entities;
using ShopVerse.BuildingBlocks.Messaging.Events;

namespace Application.Factories;

public static class OrderCheckoutEventFactory
{
    public static OrderCheckoutEvent Create(Order order)
    {
        return new OrderCheckoutEvent
        {
            OrderId = order.Id.Value,
            CustomerId = order.CustomerId.Value,
            OrderName = order.OrderName.Value,
            BillingCity = order.BillingAddress.State,
            BillingStreet = order.BillingAddress.AddressLine,
            ShippingCity = order.ShippingAddress.State,
            ShippingStreet = order.ShippingAddress.AddressLine,
            TotalPrice = order.TotalPrice.Amount,
            Items = order.OrderItems.Select(i => new OrderCheckoutItem
            {
                ProductId = i.ProductId.Value,
                Quantity = i.Quantity,
                Price = i.Price.Amount
            }).ToList()
        };
    }
}