//using Application.GrpcInterface;
using Application.Contracts;
using Domain.Contract;
using Domain.Entities;
using Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Result;

namespace Application.Sales.Commands.CreateOrder;
public class CreateOrderCommandValidator 
    : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}
public class CreateOrderCommandHandler(IUnitOfWork unitOfWork, IInventoryApiClient inventoryApiClient)//, IInventoryServiceClient inventoryClient
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{

    public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        //foreach (var item in request.Order.OrderItems)
        //{
        //    var isAvailable = await inventoryClient.IsProductAvailableAsync(item.ProductId, item.Quantity);
        //    if (!isAvailable)
        //    {
        //        throw new Exception($"موجودی محصول {item.ProductId} کافی نیست.");
        //    }
        //}
        foreach (var item in request.Order.OrderItems)
        {
            bool isAvailable = await inventoryApiClient.CheckProductAvailabilityAsync(item.ProductId, item.Quantity);

            if (!isAvailable)
            {
                return new CreateOrderResult(false, null, $"محصول با شناسه {item.ProductId} به اندازه کافی موجود نیست.");
            }
        }


        var dto = request.Order;

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
        var orderItems = dto.OrderItems.Select(item =>
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

        return new CreateOrderResult(true, null, order.Id.Value.ToString());
    }
}
