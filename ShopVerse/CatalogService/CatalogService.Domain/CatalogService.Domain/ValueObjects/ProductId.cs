using Catalog.Domain.Exceptions;

namespace Catalog.Domain.ValueObjects;

public sealed record ProductId
{
    public Guid Value { get; }

    private ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId NewId() => new ProductId(Guid.NewGuid());

    public static ProductId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("ProductId cannot be empty.");

        return new ProductId(value);
    }
    public static bool TryParse(string str, out ProductId productId)
    {
        if (Guid.TryParse(str, out var guid) && guid != Guid.Empty)
        {
            productId = new ProductId(guid);
            return true;
        }
        productId = null!;
        return false;
    }
    public override string ToString() => Value.ToString();

    public static implicit operator Guid(ProductId id) => id.Value;

    public static explicit operator ProductId(Guid id) => Of(id);
}
