namespace Catalog.Domain.ValueObjects;
public sealed record ProductMediaTypeId(Guid Value)
{
    public static ProductMediaTypeId NewId() => new(Guid.NewGuid());

    public static ProductMediaTypeId Of(Guid id) => id == Guid.Empty
        ? throw new ArgumentException("ProductMediaTypeId cannot be empty")
        : new ProductMediaTypeId(id);
}


