namespace Catalog.Domain.ValueObjects;

public sealed record ProductMediaId(Guid Value)
{
    public static ProductMediaId NewId() => new(Guid.NewGuid());

    public static ProductMediaId Of(Guid id) => id == Guid.Empty
        ? throw new ArgumentException("ProductMediaId cannot be empty")
        : new ProductMediaId(id);
}

