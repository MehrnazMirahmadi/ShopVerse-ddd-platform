using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using FluentAssertions;

namespace Sales.UnitTests.Domain.Entities;

public class OrderTests
{
    [Fact]
    public void Order_Create_WithValidData_ShouldCreateOrder()
    {
        // Arrange
        var orderId = OrderId.Of(Guid.NewGuid());
        var customerId = CustomerId.Of(Guid.NewGuid());
        var orderName = OrderName.Of("Test Order");
        var shippingAddress = Address.Of("Ali", "Test", "ali@test.com", "Tehran", "Iran", "Tehran", "12345");
        var billingAddress = Address.Of("Ali", "Test", "ali@test.com", "Tehran", "Iran", "Tehran", "12345");
        var payment = Payment.Of("Test Card", "1234567890123456", "12/26", "123", 1);

        // Act
        var order = Order.Create(orderId, customerId, orderName, shippingAddress, billingAddress, payment);

        // Assert
        order.Id.Should().Be(orderId);
        order.CustomerId.Should().Be(customerId);
        order.OrderName.Should().Be(orderName);
        order.Status.Should().Be(OrderStatus.Pending);
    }

    [Fact]
    public void Order_SetStatus_ShouldChangeStatus()
    {
        // Arrange
        var order = CreateValidOrder();

        // Act
        order.SetStatus(OrderStatus.Completed);

        // Assert
        order.Status.Should().Be(OrderStatus.Completed);
    }

    [Fact]
    public void Order_TotalPrice_WithNoItems_ShouldReturnZero()
    {
        // Arrange
        var order = CreateValidOrder();

        // Act
        var totalPrice = order.TotalPrice;

        // Assert
        totalPrice.Amount.Should().Be(0m);
        totalPrice.Currency.Should().Be("IRR");
    }

    private static Order CreateValidOrder()
    {
        var orderId = OrderId.Of(Guid.NewGuid());
        var customerId = CustomerId.Of(Guid.NewGuid());
        var orderName = OrderName.Of("Test Order");
        var shippingAddress = Address.Of("Ali", "Test", "ali@test.com", "Tehran", "Iran", "Tehran", "12345");
        var billingAddress = Address.Of("Ali", "Test", "ali@test.com", "Tehran", "Iran", "Tehran", "12345");
        var payment = Payment.Of("Test Card", "1234567890123456", "12/26", "123", 1);

        return Order.Create(orderId, customerId, orderName, shippingAddress, billingAddress, payment);
    }
} 