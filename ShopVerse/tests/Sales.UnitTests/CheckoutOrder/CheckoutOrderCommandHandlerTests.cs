using Application.GrpcInterface;
using Application.Sales.CheckoutOrder;
using CheckoutOrder.Common.Builders;
using Domain.Contract;
using Domain.Entities;
using FluentAssertions;
using MassTransit;
using Moq;

namespace CheckoutOrder;

public class CheckoutOrderCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IInventoryServiceClient> _inventoryClientMock = new();
    private readonly Mock<IPublishEndpoint> _publishEndpointMock = new();

    private CheckoutOrderCommandHandler CreateHandler()
    {
        return new CheckoutOrderCommandHandler(
            _publishEndpointMock.Object,
            _unitOfWorkMock.Object,
            _inventoryClientMock.Object);
    }
    [Fact]
    public async Task Should_ThrowException_When_InventoryIsNotEnough()
    {
        // Arrange
        var handler = CreateHandler();
        var request = FakeOrderCheckoutCommandRequest.WithOneItem(productId: Guid.NewGuid(), quantity: 5);

        _inventoryClientMock.Setup(x =>
            x.IsProductAvailableAsync(It.IsAny<Guid>(), It.IsAny<int>())
        ).ReturnsAsync(false);

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() =>
            handler.Handle(request, CancellationToken.None)
        );
    }
    [Fact]
    public async Task Should_ReturnError_When_OrderAlreadyExists()
    {
        // Arrange
        var handler = CreateHandler();
        var request = FakeOrderCheckoutCommandRequest.WithOneItem();

        // موجودی رو OK فرض می‌کنیم
        _inventoryClientMock
            .Setup(x => x.IsProductAvailableAsync(It.IsAny<Guid>(), It.IsAny<int>()))
            .ReturnsAsync(true);

        // سفارش قبلاً وجود داره
        _unitOfWorkMock
            .Setup(x => x.OrderRepository.ExistsAsync(request.orderCheckoutDto.OrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        response.IsSuccess.Should().BeFalse();
        response.ErrorMessage.Should().Contain("قبلاً ثبت شده");
    }
    [Fact]
    public async Task Should_CreateOrder_When_ValidRequest()
    {
        // Arrange
        var handler = CreateHandler();
        var request = FakeOrderCheckoutCommandRequest.WithOneItem();

        _inventoryClientMock
            .Setup(x => x.IsProductAvailableAsync(It.IsAny<Guid>(), It.IsAny<int>()))
            .ReturnsAsync(true);

        _unitOfWorkMock
            .Setup(x => x.OrderRepository.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        response.IsSuccess.Should().BeTrue();

        _unitOfWorkMock.Verify(x => x.OrderRepository.AddOrderAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);

    }

}
