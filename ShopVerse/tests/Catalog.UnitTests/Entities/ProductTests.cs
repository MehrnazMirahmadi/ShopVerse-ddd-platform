using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;

namespace Entities;

public class ProductTests
{
    [Fact]
    public void Product_Create_WithValidData_ShouldCreateProduct()
    {
        // Arrange
        var productId = ProductId.Of(Guid.NewGuid());
        var categoryId = CategoryId.Of(Guid.NewGuid());
        var name = "Test Product";
        var smallDescription = "Test Description";
        var slug = "test-product";
        var basePrice = 100m;
        var discount = 10;
        var availableCount = 5;

        // Act
        var product = Product.Create(
            id: productId,
            name: name,
            smallDescription: smallDescription,
            slug: slug,
            basePrice: basePrice,
            discount: discount,
            availableCount: availableCount,
            categoryId: categoryId);

        // Assert
        Assert.Equal(productId, product.Id);
        Assert.Equal(name, product.Name);
        Assert.Equal(smallDescription, product.SmallDescription);
        Assert.Equal(slug, product.Slug);
        Assert.Equal(basePrice, product.BasePrice);
        Assert.Equal(discount, product.Discount);
        Assert.Equal(availableCount, product.AvailableCount);
        Assert.Equal(categoryId, product.CategoryId);
    }
    [Fact]
    public void Product_Create_WithEmptyName_ShouldThrowArgumentException()
    {
        // Arrange
        var productId = ProductId.Of(Guid.NewGuid());

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            Product.Create(
                id: productId,
                name: "",
                smallDescription: "desc",
                slug: "slug",
                basePrice: 100m,
                discount: 0,
                availableCount: 10,
                categoryId: CategoryId.Of(Guid.NewGuid())));
    }
    [Fact]
    public void Product_Create_WithNonPositiveBasePrice_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Product.Create(
                id: ProductId.Of(Guid.NewGuid()),
                name: "Name",
                smallDescription: "desc",
                slug: "slug",
                basePrice: 0,
                discount: 0,
                availableCount: 10,
                categoryId: CategoryId.Of(Guid.NewGuid())));
    }
    [Fact]
    public void Product_Create_WithNegativeAvailableCount_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Product.Create(
                id: ProductId.Of(Guid.NewGuid()),
                name: "Name",
                smallDescription: "desc",
                slug: "slug",
                basePrice: 10,
                discount: 0,
                availableCount: -1,
                categoryId: CategoryId.Of(Guid.NewGuid())));
    }

    [Fact]
    public void UpdateDetails_WithValidData_ShouldUpdateProperties()
    {
        // Arrange
        var product = Product.Create(
            ProductId.Of(Guid.NewGuid()),
            "Old Name", "Old Desc", "old-slug", 50m, 5, 3, CategoryId.Of(Guid.NewGuid()));

        var newCategoryId = CategoryId.Of(Guid.NewGuid());

        // Act
        product.UpdateDetails("New Name", "New Desc", "new-slug", 200m, 20, 10, newCategoryId);

        // Assert
        Assert.Equal("New Name", product.Name);
        Assert.Equal("New Desc", product.SmallDescription);
        Assert.Equal("new-slug", product.Slug);
        Assert.Equal(200m, product.BasePrice);
        Assert.Equal(20, product.Discount);
        Assert.Equal(10, product.AvailableCount);
        Assert.Equal(newCategoryId, product.CategoryId);
    }

    [Fact]
    public void UpdateDetails_WithEmptyName_ShouldThrowArgumentException()
    {
        var product = Product.Create(
            ProductId.Of(Guid.NewGuid()),
            "Name", "Desc", "slug", 100m, 0, 10, CategoryId.Of(Guid.NewGuid()));

        Assert.Throws<ArgumentException>(() =>
            product.UpdateDetails("", "desc", "slug", 100m, 0, 10, CategoryId.Of(Guid.NewGuid())));
    }

}
