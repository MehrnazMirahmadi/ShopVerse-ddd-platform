namespace Catalog.Domain.ValueObjects;

public sealed record ProductMediaId(Guid Value)
{
    public static ProductMediaId NewId() => new(Guid.NewGuid());
}

