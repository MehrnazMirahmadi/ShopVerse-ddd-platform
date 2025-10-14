using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;

namespace Entities;

public class ProductFeatureTests
{
    [Fact]
    public void ProductFeature_Create_WithValidData_ShouldCreateFeature()
    {
        var feature = ProductFeature.Create(
            ProductFeatureId.Of(Guid.NewGuid()),
            FeatureId.Of(Guid.NewGuid()),
            "Blue",
            20,
            false);

        Assert.Equal("Blue", feature.FeatureValue);
        Assert.Equal(20, feature.EffectOnPrice);
    }

    [Fact]
    public void ProductFeature_Create_WithEmptyValue_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() =>
            ProductFeature.Create(
                ProductFeatureId.Of(Guid.NewGuid()),
                FeatureId.Of(Guid.NewGuid()),
                "",
                0,
                false));
    }
}
