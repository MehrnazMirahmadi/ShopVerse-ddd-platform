using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;

namespace Sales.UnitTests.Domain.Entities;

public class ProductTests
{
    [Fact]
    public void Product_Create_WithValidData_ShouldCreateProduct()
    {
        // Arrange
        var productId = ProductId.Of(Guid.NewGuid());
        string name = "Test Product";
        decimal price = 100.50m;

        // Act
        var product = Product.Create(productId, name, price);

        // Assert
        product.Id.Should().Be(productId);
        product.Name.Should().Be(name);
        product.Price.Should().Be(price);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Product_Create_WithEmptyName_ShouldThrowException(string name)
    {
        // Arrange
        var productId = ProductId.Of(Guid.NewGuid());
        decimal price = 100.50m;

        // Act & Assert
        var action = () => Product.Create(productId, name, price);
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Product_Create_WithInvalidPrice_ShouldThrowException(decimal price)
    {
        // Arrange
        var productId = ProductId.Of(Guid.NewGuid());
        string name = "Test Product";

        // Act & Assert
        var action = () => Product.Create(productId, name, price);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }
} 