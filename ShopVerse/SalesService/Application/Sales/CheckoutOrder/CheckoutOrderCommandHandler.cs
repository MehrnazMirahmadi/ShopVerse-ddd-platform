using Application.Contracts;
using Domain.Contract;
using Domain.Entities;
using Domain.ValueObjects;
using Mapster;
using MassTransit;
using ShopVerse.BuildingBlocks.Messaging.Events;

namespace Application.Sales.CheckoutOrder;

public class CheckoutOrderCommandHandler(IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork, IInventoryApiClient inventoryApiClient)
    : ICommandHandler<CheckoutOrderCommandRequest, CheckoutOrderCommandResponse>
{
    public async Task<CheckoutOrderCommandResponse> Handle(CheckoutOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var dto = request.orderCheckoutDto;
        foreach (var item in request.orderCheckoutDto.Items)
        {
            bool isAvailable = await inventoryApiClient.CheckProductAvailabilityAsync(item.ProductId, item.Quantity);

            if (!isAvailable)
            {
                return new CheckoutOrderCommandResponse(false, null, $"محصول با شناسه {item.ProductId} به اندازه کافی موجود نیست.");
            }
        }
      
        if (await unitOfWork.OrderRepository.ExistsAsync(dto.OrderId, cancellationToken))
        {
            return new CheckoutOrderCommandResponse(false, null, $"سفارش با شناسه {dto.OrderId} قبلاً ثبت شده است.");
        }

        var shippingAddress = Address.Of(
            dto.ShippingAddress.FirstName,
            dto.ShippingAddress.LastName,
            dto.ShippingAddress.EmailAddress,
            dto.ShippingAddress.AddressLine,
            dto.ShippingAddress.Country,
            dto.ShippingAddress.State,
            dto.ShippingAddress.ZipCode
        );
            var billingAddress = Address.Of(
               dto.BillingAddress.FirstName,
               dto.BillingAddress.LastName,
               dto.BillingAddress.EmailAddress,
               dto.BillingAddress.AddressLine,
               dto.BillingAddress.Country,
               dto.BillingAddress.State,
               dto.BillingAddress.ZipCode
           );

            var payment = Payment.Of(
                dto.Payment.CardName,
                dto.Payment.CardNumber,
                dto.Payment.Expiration,
                dto.Payment.Cvv,
                dto.Payment.PaymentMethod
            );

            var order = Order.Create(
            OrderId.Of(Guid.NewGuid()),
            CustomerId.Of(dto.CustomerId),
            OrderName.Of(dto.OrderName),
            shippingAddress,
            billingAddress,
            payment);
            var orderItems = dto.Items.Select(item =>
         OrderItem.Create(
             OrderItemId.Of(Guid.NewGuid()),
             order.Id,
             ProductId.Of(item.ProductId),
             item.Quantity,
             Money.Of(item.Price, "IRR")
         )).ToList();
            order.SetOrderItems(orderItems);
            await unitOfWork.OrderRepository.AddOrderAsync(order, cancellationToken);
            await unitOfWork.SaveChangesAsync();
       


        var eventMessage = request.orderCheckoutDto.Adapt<OrderCheckoutEvent>();
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        return new CheckoutOrderCommandResponse(true, null, dto.OrderId.ToString());
    }
}
