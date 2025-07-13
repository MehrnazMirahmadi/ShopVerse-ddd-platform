using Application.Contracts;
using Application.Dtos;
using Application.Sales.Commands.CreateOrder;
using Domain.Contract;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Sales.UnitTests.Application.Commands.CreateOrder;

public class CreateOrderCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IInventoryApiClient> _inventoryApiClientMock = new();

    private CreateOrderCommandHandler CreateHandler()
    {
        return new CreateOrderCommandHandler(_unitOfWorkMock.Object, _inventoryApiClientMock.Object);
    }

    [Fact]
    public async Task Handle_WithUnavailableProduct_ShouldReturnFailure()
    {
        // Arrange
        var handler = CreateHandler();
        var request = CreateValidCreateOrderCommand();

        _inventoryApiClientMock
            .Setup(x => x.CheckProductAvailabilityAsync(It.IsAny<Guid>(), It.IsAny<int>()))
            .ReturnsAsync(false);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("موجود نیست");
        result.Id.Should().BeNull();

        _unitOfWorkMock.Verify(x => x.OrderRepository.AddOrderAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
    }


    [Fact]
    public async Task Handle_WithInventoryServiceFailure_ShouldReturnFailure()
    {
        // Arrange
        var handler = CreateHandler();
        var request = CreateValidCreateOrderCommand();

        _inventoryApiClientMock
            .Setup(x => x.CheckProductAvailabilityAsync(It.IsAny<Guid>(), It.IsAny<int>()))
            .ReturnsAsync(false);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Contain("موجود نیست");
    }

    private static CreateOrderCommand CreateValidCreateOrderCommand()
    {
        return new CreateOrderCommand(new OrderDto(
            Id: Guid.NewGuid(),
            CustomerId: Guid.NewGuid(),
            ProductId: Guid.NewGuid(),
            OrderName: "Test Order",
            ShippingAddress: new AddressDto("Ali", "Test", "ali@test.com", "Tehran", "Iran", "Tehran", "12345"),
            BillingAddress: new AddressDto("Ali", "Test", "ali@test.com", "Tehran", "Iran", "Tehran", "12345"),
            Payment: new PaymentDto("Test Card", "1234567890123456", "12/26", "123", 1),
            Status: Domain.Enums.OrderStatus.Pending,
            OrderItems: new List<OrderItemDto>
            {
                new(Guid.NewGuid(), 2, 100.00m)
            }
        ));
    }

}