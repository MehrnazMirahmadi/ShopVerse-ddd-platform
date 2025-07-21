using Catalog.Application.Dtos;
using Catalog.Application.Products.Commands.UpdateProduct;
using Catalog.Application.Services;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Commands.UpdateProduct;

public class UpdateProductCommandHandlerTests
{
    private readonly Mock<IProductService> _productServiceMock;

    public UpdateProductCommandHandlerTests()
    {
        _productServiceMock = new Mock<IProductService>();
    }

    private UpdateProductCommandHandler UpdateHandler()
    {
        return new UpdateProductCommandHandler(_productServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenProductIsUpdated()
    {
        // Arrange
        var handler = UpdateHandler();
        var productId = ProductId.Of(Guid.NewGuid());
        var updateDto = new UpdateProductDto
        {
            Name = "Updated Name",
            SmallDescription = "Updated Desc",
            Slug = "updated-slug",
            BasePrice = 100,
            Discount = 10,
            AvailableCount = 5,
            CategoryId = Guid.NewGuid().ToString(),
            Features = new(),
            Media = new()
        };
        var command = new UpdateProductCommand(productId, updateDto);

        var product = Product.Create(
            productId,
            "Old Name",
            "Old Desc",
            "old-slug",
            50,
            0,
            10,
            new CategoryId(Guid.NewGuid())
        );

        _productServiceMock
            .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        _productServiceMock
            .Setup(x => x.UpdateProductAsync(product, updateDto, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.ProductId.Should().Be(productId);
        result.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenProductNotFound()
    {
        // Arrange
        var handler = UpdateHandler();
        var productId = ProductId.Of(Guid.NewGuid());
        var updateDto = new UpdateProductDto
        {
            Name = "Updated Name",
            SmallDescription = "Updated Desc",
            Slug = "updated-slug",
            BasePrice = 100,
            Discount = 10,
            AvailableCount = 5,
            CategoryId = Guid.NewGuid().ToString(),
            Features = new(),
            Media = new()
        };
        var command = new UpdateProductCommand(productId, updateDto);

        _productServiceMock
            .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ProductId.Should().BeNull();
        result.ErrorMessage.Should().Contain("not found");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var handler = UpdateHandler();
        var productId = ProductId.Of(Guid.NewGuid());
        var updateDto = new UpdateProductDto
        {
            Name = "Updated Name",
            SmallDescription = "Updated Desc",
            Slug = "updated-slug",
            BasePrice = 100,
            Discount = 10,
            AvailableCount = 5,
            CategoryId = Guid.NewGuid().ToString(),
            Features = new(),
            Media = new()
        };
        var command = new UpdateProductCommand(productId, updateDto);

        var product = Product.Create(
            productId,
            "Old Name",
            "Old Desc",
            "old-slug",
            50,
            0,
            10,
            new CategoryId(Guid.NewGuid())
        );

        _productServiceMock
            .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        _productServiceMock
            .Setup(x => x.UpdateProductAsync(product, updateDto, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Update failed"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ProductId.Should().BeNull();
        result.ErrorMessage.Should().Contain("Update failed");
    }
}