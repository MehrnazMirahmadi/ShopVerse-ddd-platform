namespace Catalog.Domain.ValueObjects;

public sealed record ProductFeatureId(Guid Value)
{
    public static ProductFeatureId NewId() => new(Guid.NewGuid());
    public static ProductFeatureId Of(Guid id) => id == Guid.Empty
        ? throw new ArgumentException("ProductFeatureId cannot be empty")
        : new ProductFeatureId(id);
}

