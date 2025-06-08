namespace Application.Sales.CheckoutOrder;

public record CheckoutOrderCommandRequest(OrderCheckoutDto orderCheckoutDto)
    : ICommand<CheckoutOrderCommandResponse>;
public record CheckoutOrderCommandResponse(bool IsSuccess ,Guid? Id = null, string ErrorMessage = null);

