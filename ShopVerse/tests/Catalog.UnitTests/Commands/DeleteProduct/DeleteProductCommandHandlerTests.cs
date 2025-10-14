using Catalog.Application.Products.Commands.DeleteProduct;
using Catalog.Application.Services;
using Catalog.Domain.ValueObjects;
using Commands.TestHelpers;
using Moq;

namespace Commands.DeleteProduct;

public class DeleteProductCommandHandlerTests
{
    private readonly Mock<IProductService> _productServiceMock;

    public DeleteProductCommandHandlerTests()
    {
        _productServiceMock = new Mock<IProductService>();
    }

    private DeleteProductCommandHandler CreateHandler()
    {
        return new DeleteProductCommandHandler(_productServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenProductIsDeleted()
    {
        // Arrange
        var handler = CreateHandler();
        var productId = ProductId.Of(Guid.NewGuid());
        var command = new DeleteProductCommand(productId);
        var fakeProduct = TestProductFactory.CreateFakeProduct();


        _productServiceMock
            .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeProduct);

        _productServiceMock
            .Setup(x => x.DeleteProductAsync(productId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(productId, result.ProductId);
        Assert.Null(result.ErrorMessage);
    }
    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var handler = CreateHandler();
        var productId = ProductId.Of(Guid.NewGuid());
        var command = new DeleteProductCommand(productId);

        var fakeProduct = TestProductFactory.CreateFakeProduct();

        _productServiceMock
            .Setup(x => x.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeProduct);

        _productServiceMock
            .Setup(x => x.DeleteProductAsync(productId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database failure"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.ProductId);
        Assert.Contains("An error occurred while deleting the product", result.ErrorMessage);
    }

}
