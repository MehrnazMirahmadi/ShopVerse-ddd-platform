using Application.Sales.EventHandlers.Domain;
using Domain.Entities;
using Domain.Events;
using Domain.ValueObjects;
using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;

namespace Sales.UnitTests.Application.EventHandlers;

public class OrderCreatedEventHandlerTests
{
    private readonly Mock<IPublishEndpoint> _publishEndpointMock = new();
    private readonly Mock<ILogger<OrderCreatedEventHandler>> _loggerMock = new();

    private OrderCreatedEventHandler CreateHandler()
    {
        return new OrderCreatedEventHandler(_publishEndpointMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidEvent_ShouldPublishIntegrationEvent()
    {
        // Arrange
        var handler = CreateHandler();
        var order = CreateValidOrder();
        var domainEvent = new OrderCreatedEvent(order);

        // Act
        await handler.Handle(domainEvent, CancellationToken.None);

        // Assert
        _publishEndpointMock.Verify(
            x => x.Publish(It.IsAny<OrderCreatedEvent>(), It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithValidEvent_ShouldLogInformation()
    {
        // Arrange
        var handler = CreateHandler();
        var order = CreateValidOrder();
        var domainEvent = new OrderCreatedEvent(order);

        // Act
        await handler.Handle(domainEvent, CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Domain Event handled")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithNullEvent_ShouldNotThrowException()
    {
        // Arrange
        var handler = CreateHandler();
        OrderCreatedEvent? domainEvent = null;

        // Act & Assert
        var action = () => handler.Handle(domainEvent!, CancellationToken.None);
        await action.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Handle_WithPublishEndpointException_ShouldNotThrowException()
    {
        // Arrange
        var handler = CreateHandler();
        var order = CreateValidOrder();
        var domainEvent = new OrderCreatedEvent(order);

        _publishEndpointMock
            .Setup(x => x.Publish(It.IsAny<OrderCreatedEvent>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Publish failed"));

        // Act & Assert
        var action = () => handler.Handle(domainEvent, CancellationToken.None);
        await action.Should().NotThrowAsync();
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