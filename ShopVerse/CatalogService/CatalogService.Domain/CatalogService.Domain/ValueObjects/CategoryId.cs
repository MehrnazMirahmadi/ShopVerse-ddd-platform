namespace Catalog.Domain.ValueObjects;


public sealed record CategoryId(Guid Value)
{
    public static CategoryId NewId() => new(Guid.NewGuid());
    public static CategoryId Of(Guid id) => id == Guid.Empty
        ? throw new ArgumentException("CategoryId cannot be empty")
        : new CategoryId(id);
}