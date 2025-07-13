using Application.Dtos;
using Application.Sales.CheckoutOrder;

namespace CheckoutOrder.Common.Builders;

public static class FakeOrderCheckoutCommandRequest
{
    public static CheckoutOrderCommandRequest WithOneItem(Guid? productId = null, int quantity = 1)
    {
        return new CheckoutOrderCommandRequest(
            new OrderCheckoutDto
            {
                OrderId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                OrderName = "Test Order",
                Items = new List<OrderCheckoutItemDto>
                {
                    new OrderCheckoutItemDto {
                  
                        ProductId = productId ?? Guid.NewGuid(),
                  
                        Quantity = quantity,
                  
                        Price = 100000
    }
                },
                ShippingAddress = new AddressDto
                (
                    "Ali",
                    "Test",
                   "ali@test.com",
                     "Tehran",
                    "Iran",
                    "Tehran",
                    "12345"
                ),
                BillingAddress = new AddressDto
                (
                      "Ali",
                    "Test",
                   "ali@test.com",
                     "Tehran",
                    "Iran",
                    "Tehran",
                    "12345"
                ),
                Payment = new PaymentDto
                (
                    "Test Card",
                     "1234567890123456",
                    "12/26",
                    "123",
                     1
                )
            }
        );
    }
    public static CheckoutOrderCommandRequest WithMultipleItems(int itemCount = 3)
    {
        var items = new List<OrderCheckoutItemDto>();

        for (int i = 0; i < itemCount; i++)
        {
            items.Add(new OrderCheckoutItemDto
            {
                ProductId = Guid.NewGuid(),
                Quantity = i + 1,
                Price = 10000 * (i + 1)
            });
        }

        var dto = new OrderCheckoutDto
        {
            OrderId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            OrderName = "Test Multi-Item Order",
            Items = items,
            ShippingAddress = new AddressDto("Ali", "Test", "ali@test.com", "Tehran", "Iran", "Tehran", "12345"),
            BillingAddress = new AddressDto("Ali", "Test", "ali@test.com", "Tehran", "Iran", "Tehran", "12345"),
            Payment = new PaymentDto("Test Card", "1234567890123456", "12/26", "123", 1)
        };

        return new CheckoutOrderCommandRequest(dto);
    }


}
