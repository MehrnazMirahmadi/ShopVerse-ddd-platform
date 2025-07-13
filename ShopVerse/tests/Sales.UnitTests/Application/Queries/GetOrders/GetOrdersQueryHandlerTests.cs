using Application.Sales.Queries.GetOrders;
using Domain.Contract;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Moq;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Result;

namespace Sales.UnitTests.Application.Queries.GetOrders;

public class GetOrdersQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private GetOrdersQueryHandler CreateHandler()
    {
        return new GetOrdersQueryHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldReturnOrders()
    {
        // Arrange
        var handler = CreateHandler();
        var paging = new PaginationRequest(1, 10);
        var query = new GetOrdersQuery(paging);

        var orders = CreateSampleOrders();
        var paginationResult = new PaginationResult<Order>(10, 1, orders.Count, orders);

        _unitOfWorkMock
            .Setup(x => x.OrderRepository.GetAllAsync(It.IsAny<Application.Specifications.OrdersSpecification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginationResult);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Data.Should().HaveCount(orders.Count);
    }

    [Fact]
    public async Task Handle_WithNoOrders_ShouldReturnFailure()
    {
        // Arrange
        var handler = CreateHandler();
        var paging = new PaginationRequest(1, 10);
        var query = new GetOrdersQuery(paging);

        var emptyPaginationResult = new PaginationResult<Order>(10, 1, 0, new List<Order>());

        _unitOfWorkMock
            .Setup(x => x.OrderRepository.GetAllAsync(It.IsAny<Application.Specifications.OrdersSpecification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyPaginationResult);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("سفارشی موجود نیست");
    }

    [Fact]
    public async Task Handle_WithFilterName_ShouldApplyFilter()
    {
        // Arrange
        var handler = CreateHandler();
        var paging = new PaginationRequest(1, 10);
        var query = new GetOrdersQuery(paging, "Test");

        var orders = CreateSampleOrders();
        var paginationResult = new PaginationResult<Order>(10, 1, orders.Count, orders);

        _unitOfWorkMock
            .Setup(x => x.OrderRepository.GetAllAsync(It.IsAny<Application.Specifications.OrdersSpecification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginationResult);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _unitOfWorkMock.Verify(x => x.OrderRepository.GetAllAsync(It.IsAny<Application.Specifications.OrdersSpecification>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithSorting_ShouldApplySorting()
    {
        // Arrange
        var handler = CreateHandler();
        var paging = new PaginationRequest(1, 10);
        var query = new GetOrdersQuery(paging, null, true); // SortByQuantityDesc = true

        var orders = CreateSampleOrders();
        var paginationResult = new PaginationResult<Order>(10, 1, orders.Count, orders);

        _unitOfWorkMock
            .Setup(x => x.OrderRepository.GetAllAsync(It.IsAny<Application.Specifications.OrdersSpecification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginationResult);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _unitOfWorkMock.Verify(x => x.OrderRepository.GetAllAsync(It.IsAny<Application.Specifications.OrdersSpecification>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    private static List<Order> CreateSampleOrders()
    {
        var orders = new List<Order>();
        
        for (int i = 1; i <= 3; i++)
        {
            var orderId = OrderId.Of(Guid.NewGuid());
            var customerId = CustomerId.Of(Guid.NewGuid());
            var orderName = OrderName.Of($"Test Order {i}");
            var shippingAddress = Address.Of("Ali", "Test", "ali@test.com", "Tehran", "Iran", "Tehran", "12345");
            var billingAddress = Address.Of("Ali", "Test", "ali@test.com", "Tehran", "Iran", "Tehran", "12345");
            var payment = Payment.Of("Test Card", "1234567890123456", "12/26", "123", 1);

            var order = Order.Create(orderId, customerId, orderName, shippingAddress, billingAddress, payment);
            
            // Add some order items
            order.AddOrderItem(OrderItemId.Of(Guid.NewGuid()), ProductId.Of(Guid.NewGuid()), i, Money.Of(100.00m));
            
            orders.Add(order);
        }

        return orders;
    }
} 