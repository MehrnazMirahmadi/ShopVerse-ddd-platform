using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Abstractions;

namespace Catalog.Domain.Entities;

public class ProductFeature : Entity<ProductFeatureId>
{
    public FeatureId FeatureId { get; private set; }
    public string FeatureValue { get; private set; }
    public int EffectOnPrice { get; private set; }
    public bool IsDefault { get; private set; }

    private ProductFeature(ProductFeatureId id, FeatureId featureId, string featureValue, int effectOnPrice, bool isDefault)
    {
        Id = id;
        FeatureId = featureId;
        FeatureValue = featureValue;
        EffectOnPrice = effectOnPrice;
        IsDefault = isDefault;
    }

    public static ProductFeature Create(ProductFeatureId id, FeatureId featureId, string featureValue, int effectOnPrice, bool isDefault)
    {
        if (string.IsNullOrWhiteSpace(featureValue)) throw new ArgumentException("FeatureValue is required");
        return new ProductFeature(id, featureId, featureValue, effectOnPrice, isDefault);
    }
}
