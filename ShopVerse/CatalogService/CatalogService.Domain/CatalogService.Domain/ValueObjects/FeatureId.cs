namespace Catalog.Domain.ValueObjects;

public sealed record FeatureId(Guid Value)
{
    public static FeatureId NewId() => new(Guid.NewGuid());
    public static FeatureId Of(Guid id) => id == Guid.Empty
        ? throw new ArgumentException("FeatureId cannot be empty")
        : new FeatureId(id);
}
