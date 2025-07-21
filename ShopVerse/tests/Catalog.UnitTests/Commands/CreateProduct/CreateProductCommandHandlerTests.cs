using Catalog.Application.Dtos;
using Catalog.Application.Products.Commands.CreateProduct;
using Catalog.Application.Services;
using Catalog.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Commands.CreateProduct;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductService> _productServiceMock;

    public CreateProductCommandHandlerTests()
    {
        _productServiceMock = new Mock<IProductService>();
    }

    private CreateProductCommandHandler CreateHandler()
    {
        return new CreateProductCommandHandler(_productServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenProductIsCreated()
    {
        // Arrange
        var handler = CreateHandler();
        var productId = ProductId.Of(Guid.NewGuid()); // Ensure ProductId is properly instantiated
        var productDto = new ProductDto();
        var command = new CreateProductCommand(productDto);

        _productServiceMock
            .Setup(x => x.CreateProductAsync(productDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productId); // Ensure ReturnsAsync matches the expected type

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Id.Should().Be(productId);
        result.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var handler = CreateHandler();
        var productDto = new ProductDto();
        var command = new CreateProductCommand(productDto);

        _productServiceMock
            .Setup(x => x.CreateProductAsync(productDto, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Failed to create product"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Id.Should().BeNull();
        result.ErrorMessage.Should().Contain("Failed to create product");
    }
}
