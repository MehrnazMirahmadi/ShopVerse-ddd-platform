namespace Application.Sales.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order)
    : ICommand<CreateOrderResult>;

public record CreateOrderResult(bool IsSuccess, Guid? Id = null, string ErrorMessage = null);

