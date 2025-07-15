namespace Catalog.Domain.ValueObjects;

public sealed record ProductFeatureId(Guid Value)
{
    public static ProductFeatureId NewId() => new(Guid.NewGuid());
}
