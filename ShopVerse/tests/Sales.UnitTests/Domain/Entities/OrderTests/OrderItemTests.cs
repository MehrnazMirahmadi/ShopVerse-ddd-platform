using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;

namespace Sales.UnitTests.Domain.Entities;

public class OrderItemTests
{
    [Fact]
    public void OrderItem_Create_WithValidData_ShouldCreateOrderItem()
    {
        // Arrange
        var orderItemId = OrderItemId.Of(Guid.NewGuid());
        var orderId = OrderId.Of(Guid.NewGuid());
        var productId = ProductId.Of(Guid.NewGuid());
        var quantity = 2;
        var price = Money.Of(100.00m);

        // Act
        var orderItem = OrderItem.Create(orderItemId, orderId, productId, quantity, price);

        // Assert
        orderItem.Id.Should().Be(orderItemId);
        orderItem.OrderId.Should().Be(orderId);
        orderItem.ProductId.Should().Be(productId);
        orderItem.Quantity.Should().Be(quantity);
        orderItem.Price.Should().Be(price);
    }

    [Fact]
    public void OrderItem_Create_WithZeroQuantity_ShouldCreateOrderItem()
    {
        // Arrange
        var orderItemId = OrderItemId.Of(Guid.NewGuid());
        var orderId = OrderId.Of(Guid.NewGuid());
        var productId = ProductId.Of(Guid.NewGuid());
        var quantity = 0;
        var price = Money.Of(100.00m);

        // Act
        var orderItem = OrderItem.Create(orderItemId, orderId, productId, quantity, price);

        // Assert
        orderItem.Quantity.Should().Be(quantity);
    }

    [Fact]
    public void OrderItem_Create_WithNegativeQuantity_ShouldCreateOrderItem()
    {
        // Arrange
        var orderItemId = OrderItemId.Of(Guid.NewGuid());
        var orderId = OrderId.Of(Guid.NewGuid());
        var productId = ProductId.Of(Guid.NewGuid());
        var quantity = -1;
        var price = Money.Of(100.00m);

        // Act
        var orderItem = OrderItem.Create(orderItemId, orderId, productId, quantity, price);

        // Assert
        orderItem.Quantity.Should().Be(quantity);
    }

    [Fact]
    public void OrderItem_Create_WithHighQuantity_ShouldCreateOrderItem()
    {
        // Arrange
        var orderItemId = OrderItemId.Of(Guid.NewGuid());
        var orderId = OrderId.Of(Guid.NewGuid());
        var productId = ProductId.Of(Guid.NewGuid());
        var quantity = 999999;
        var price = Money.Of(100.00m);

        // Act
        var orderItem = OrderItem.Create(orderItemId, orderId, productId, quantity, price);

        // Assert
        orderItem.Quantity.Should().Be(quantity);
    }

    [Fact]
    public void OrderItem_Create_WithZeroPrice_ShouldCreateOrderItem()
    {
        // Arrange
        var orderItemId = OrderItemId.Of(Guid.NewGuid());
        var orderId = OrderId.Of(Guid.NewGuid());
        var productId = ProductId.Of(Guid.NewGuid());
        var quantity = 1;
        var price = Money.Of(0.00m);

        // Act
        var orderItem = OrderItem.Create(orderItemId, orderId, productId, quantity, price);

        // Assert
        orderItem.Price.Should().Be(price);
    }

    [Fact]
    public void OrderItem_Create_WithDifferentCurrency_ShouldCreateOrderItem()
    {
        // Arrange
        var orderItemId = OrderItemId.Of(Guid.NewGuid());
        var orderId = OrderId.Of(Guid.NewGuid());
        var productId = ProductId.Of(Guid.NewGuid());
        var quantity = 1;
        var price = new Money(100.00m, "USD");

        // Act
        var orderItem = OrderItem.Create(orderItemId, orderId, productId, quantity, price);

        // Assert
        orderItem.Price.Should().Be(price);
        orderItem.Price.Currency.Should().Be("USD");
    }
}