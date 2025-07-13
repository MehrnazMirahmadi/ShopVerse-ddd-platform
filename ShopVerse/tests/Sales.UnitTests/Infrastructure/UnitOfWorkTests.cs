using Domain.Contract;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Sales.UnitTests.Infrastructure;

public class UnitOfWorkTests : IDisposable
{
    private readonly OrderDbContext _context;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkTests()
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new OrderDbContext(options);
        _orderRepository = new OrderRepository(_context);
        _unitOfWork = new UnitOfWork(_context, _orderRepository);
    }

    [Fact]
    public void UnitOfWork_InitialState_ShouldNotHaveActiveTransaction()
    {
        // Assert
        _unitOfWork.HasActiveTransaction.Should().BeFalse();
    }

    [Fact]
    public async Task UnitOfWork_BeginTransactionAsync_ShouldStartTransaction()
    {
        // Act
        await _unitOfWork.BeginTransactionAsync();

        // Assert
        _unitOfWork.HasActiveTransaction.Should().BeTrue();
    }

    [Fact]
    public void UnitOfWork_OrderRepository_ShouldReturnRepository()
    {
        // Assert
        _unitOfWork.OrderRepository.Should().NotBeNull();
        _unitOfWork.OrderRepository.Should().BeOfType<OrderRepository>();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}