using Infrastructure.Persistence.Context;

namespace Repositories;

public class OrderRepositoryTests : IDisposable
{
    private readonly OrderDbContext _context;
    private readonly IOrderRepository _repository;

    public OrderRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new OrderDbContext(options);
        _repository = new OrderRepository(_context);
    }

    [Fact]
    public async Task AddOrderAsync_WithValidOrder_ShouldSaveToDatabase()
    {
        // Arrange
        var order = CreateValidOrder();

        // Act
        await _repository.AddOrderAsync(order, CancellationToken.None);
        await _context.SaveChangesAsync();

        // Assert
        var savedOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);
        savedOrder.Should().NotBeNull();
        savedOrder!.Id.Should().Be(order.Id);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingOrder_ShouldReturnTrue()
    {
        // Arrange
        var order = CreateValidOrder();
        await _repository.AddOrderAsync(order, CancellationToken.None);
        await _context.SaveChangesAsync();

        // Act
        var exists = await _repository.ExistsAsync(order.Id, CancellationToken.None);

        // Assert
        exists.Should().BeTrue();
    }
