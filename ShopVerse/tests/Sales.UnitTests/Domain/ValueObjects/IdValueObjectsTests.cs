using Domain.ValueObjects;
using FluentAssertions;

namespace Sales.UnitTests.Domain.ValueObjects;

public class IdValueObjectsTests
{
    [Fact]
    public void OrderId_Of_WithValidGuid_ShouldCreateOrderId()
    {
        // Arrange
        var validGuid = Guid.NewGuid();

        // Act
        var orderId = OrderId.Of(validGuid);

        // Assert
        orderId.Value.Should().Be(validGuid);
    }

    [Fact]
    public void OrderId_Of_WithEmptyGuid_ShouldThrowException()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act & Assert
        var action = () => OrderId.Of(emptyGuid);
        action.Should().Throw<DomainException>()
            .WithMessage("OrderId cannot be empty.");
    }

    [Fact]
    public void CustomerId_Of_WithValidGuid_ShouldCreateCustomerId()
    {
        // Arrange
        var validGuid = Guid.NewGuid();

        // Act
        var customerId = CustomerId.Of(validGuid);

        // Assert
        customerId.Value.Should().Be(validGuid);
    }

    [Fact]
    public void CustomerId_Of_WithEmptyGuid_ShouldThrowException()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act & Assert
        var action = () => CustomerId.Of(emptyGuid);
        action.Should().Throw<DomainException>()
            .WithMessage("CustomerId cannot be empty.");
    }

    [Fact]
    public void ProductId_Of_WithValidGuid_ShouldCreateProductId()
    {
        // Arrange
        var validGuid = Guid.NewGuid();

        // Act
        var productId = ProductId.Of(validGuid);

        // Assert
        productId.Value.Should().Be(validGuid);
    }

    [Fact]
    public void ProductId_Of_WithEmptyGuid_ShouldThrowException()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act & Assert
        var action = () => ProductId.Of(emptyGuid);
        action.Should().Throw<DomainException>()
            .WithMessage("OrderItemId cannot be empty.");
    }

    [Fact]
    public void OrderItemId_Of_WithValidGuid_ShouldCreateOrderItemId()
    {
        // Arrange
        var validGuid = Guid.NewGuid();

        // Act
        var orderItemId = OrderItemId.Of(validGuid);

        // Assert
        orderItemId.Value.Should().Be(validGuid);
    }

    [Fact]
    public void OrderItemId_Of_WithEmptyGuid_ShouldThrowException()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act & Assert
        var action = () => OrderItemId.Of(emptyGuid);
        action.Should().Throw<DomainException>()
            .WithMessage("OrderItemId cannot be empty.");
    }

    [Fact]
    public void OrderId_ImplicitConversion_ToGuid_ShouldWork()
    {
        // Arrange
        var validGuid = Guid.NewGuid();
        var orderId = OrderId.Of(validGuid);

        // Act
        Guid convertedGuid = orderId;

        // Assert
        convertedGuid.Should().Be(validGuid);
    }

    [Fact]
    public void ProductId_ImplicitConversion_ToGuid_ShouldWork()
    {
        // Arrange
        var validGuid = Guid.NewGuid();
        var productId = ProductId.Of(validGuid);

        // Act
        Guid convertedGuid = productId;

        // Assert
        convertedGuid.Should().Be(validGuid);
    }

    [Fact]
    public void OrderId_ExplicitConversion_FromGuid_ShouldWork()
    {
        // Arrange
        var validGuid = Guid.NewGuid();

        // Act
        var orderId = (OrderId)validGuid;

        // Assert
        orderId.Value.Should().Be(validGuid);
    }

    [Fact]
    public void ProductId_ExplicitConversion_FromGuid_ShouldWork()
    {
        // Arrange
        var validGuid = Guid.NewGuid();

        // Act
        var productId = (ProductId)validGuid;

        // Assert
        productId.Value.Should().Be(validGuid);
    }
} 