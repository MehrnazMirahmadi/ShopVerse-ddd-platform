using Application.Factories;
using Application.GrpcInterface;
using Domain.Contract;
using Domain.Entities;
using Domain.ValueObjects;
using MassTransit;

namespace Application.Sales.CheckoutOrder;

public class CheckoutOrderCommandHandler
    (IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork, IInventoryServiceClient inventoryClient)
    : ICommandHandler<CheckoutOrderCommandRequest, CheckoutOrderCommandResponse>
{
    public async Task<CheckoutOrderCommandResponse> Handle(CheckoutOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var dto = request.orderCheckoutDto;
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        if (request.orderCheckoutDto == null)
            throw new ArgumentNullException(nameof(request.orderCheckoutDto));
        if (request.orderCheckoutDto.Items == null)
            throw new ArgumentNullException(nameof(request.orderCheckoutDto.Items));
        foreach (var item in request.orderCheckoutDto.Items)
        {
            var isAvailable = await inventoryClient.IsProductAvailableAsync(item.ProductId, item.Quantity);
            if (!isAvailable)
            {
                throw new Exception($"موجودی محصول {item.ProductId} کافی نیست.");
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
         payment
     );

        // ساخت آیتم‌ها
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


        var eventMessage = OrderCheckoutEventFactory.Create(order);
        await publishEndpoint.Publish(eventMessage, cancellationToken);


        return new CheckoutOrderCommandResponse(true, null, order.Id.Value.ToString());


    }
}
