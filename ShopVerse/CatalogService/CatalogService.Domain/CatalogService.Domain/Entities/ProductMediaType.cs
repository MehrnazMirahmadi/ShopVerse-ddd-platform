using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Abstractions;

namespace Catalog.Domain.Entities;

public class ProductMediaType : Aggregate<ProductMediaTypeId>
{
    public string Name { get; private set; }
    private readonly List<ProductMedia> _medias = new();
    public IReadOnlyCollection<ProductMedia> Medias => _medias.AsReadOnly();

    private ProductMediaType(ProductMediaTypeId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static ProductMediaType Create(ProductMediaTypeId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        return new ProductMediaType(id, name);
    }

    public void AddMedia(ProductMedia media)
    {
        _medias.Add(media);
    }
}
