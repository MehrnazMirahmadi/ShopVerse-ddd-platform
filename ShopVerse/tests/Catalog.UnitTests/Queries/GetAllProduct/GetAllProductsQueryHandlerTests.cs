using Catalog.Application.Dtos;
using Catalog.Application.GrpcInterface;
using Catalog.Application.Products.Queries;
using Catalog.Domain.Contract.Repositories;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Moq;
using ShopVerse.BuildingBlocks.Paging;
using ShopVerse.BuildingBlocks.Result;
using ShopVerse.BuildingBlocks.Specifications;
using Xunit;

namespace Queries.GetAllProduct;

public class GetAllProductsQueryHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IProductInventoryGrpcClient> _inventoryGrpcClientMock;

    public GetAllProductsQueryHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _inventoryGrpcClientMock = new Mock<IProductInventoryGrpcClient>();
    }

    private GetAllProductsQueryHandler GetHandler()
    {
        return new GetAllProductsQueryHandler(
            _productRepositoryMock.Object,
            _inventoryGrpcClientMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsSuccessResult_WithProductDtos()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();
        var products = new List<Product>
        {
            Product.Create(
                ProductId.Of(productId),
                "Test Product",
                "Short desc",
                "test-product",
                100m,
                10,
                0,
                CategoryId.Of(categoryId)
            )
        };
        var pagingResult = new PaginationResult<Product>(1, 1, 1, products);
        _productRepositoryMock
            .Setup(r => r.GetAllProductAsync(It.IsAny<BaseSpecification<Product>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pagingResult);

        _inventoryGrpcClientMock
            .Setup(c => c.GetAvailableQuantitiesAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(new Dictionary<Guid, int> { { productId, 5 } });

        var handler = GetHandler();
        var query = new GetAllProductsQuery(
            new PaginationRequest { PageNumber = 1, PageSize = 10 },
            null,
            false
        );

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Single(result.Value.Data);
        var dto = result.Value.Data.First();
        Assert.Equal(productId.ToString(), dto.Id);
        Assert.Equal("Test Product", dto.Name);
        Assert.Equal(5, dto.AvailableCount);
    }

    [Fact]
    public async Task Handle_ReturnsSuccessResult_WithEmptyList_WhenNoProducts()
    {
        // Arrange
        var pagingResult = new PaginationResult<Product>(10, 1, 0, new List<Product>());
        _productRepositoryMock
            .Setup(r => r.GetAllProductAsync(It.IsAny<BaseSpecification<Product>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pagingResult);

        _inventoryGrpcClientMock
            .Setup(c => c.GetAvailableQuantitiesAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(new Dictionary<Guid, int>());

        var handler = GetHandler();
      
        var query = new GetAllProductsQuery(
            new PaginationRequest { PageNumber = 1, PageSize = 10 }, 
            null, 
            false 
        );

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value.Data);
    }

    [Fact]
    public async Task Handle_ReturnsFailureResult_WhenRepositoryThrows()
    {
        // Arrange
        _productRepositoryMock
            .Setup(r => r.GetAllProductAsync(It.IsAny<BaseSpecification<Product>>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        var handler = GetHandler();
        var query = new GetAllProductsQuery(
                   new PaginationRequest { PageNumber = 1, PageSize = 10 },
                   null,
                   false
               );

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Database error", result.Message);
    }
}