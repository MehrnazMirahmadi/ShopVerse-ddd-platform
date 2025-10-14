using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;

namespace Commands.TestHelpers;

public static class TestProductFactory
{
    public static Product CreateFakeProduct()
    {
        return Product.Create(
            id: ProductId.Of(Guid.NewGuid()),
            name: "Test Product",
            smallDescription: "Test Description",
            slug: "test-product",
            basePrice: 100m,
            discount: 10,
            availableCount: 5,
            categoryId: CategoryId.Of(Guid.NewGuid())
        );
    }
}
