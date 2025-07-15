namespace Catalog.Domain.ValueObjects;
public sealed record ProductMediaTypeId(Guid Value)
{
    public static ProductMediaTypeId NewId() => new(Guid.NewGuid());
}