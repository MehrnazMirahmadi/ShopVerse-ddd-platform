using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Abstractions;

namespace Catalog.Domain.Entities;
public class Product : Aggregate<ProductId>
{
    public string Name { get; private set; }
    public string SmallDescription { get; private set; }
    public string Slug { get; private set; }
    public decimal BasePrice { get; private set; }
    public int Discount { get; private set; }
    public int AvailableCount { get; private set; }
    public CategoryId CategoryId { get; private set; }

    private readonly List<ProductFeature> _features = new();
    public IReadOnlyCollection<ProductFeature> Features => _features.AsReadOnly();

    private readonly List<ProductMedia> _media = new();
    public IReadOnlyCollection<ProductMedia> Media => _media.AsReadOnly();
    private Product() { }
    private Product(ProductId id, string name, string smallDescription, string slug, decimal basePrice, int discount, int availableCount, CategoryId categoryId)
    {
        Id = id;
        Name = name;
        SmallDescription = smallDescription;
        Slug = slug;
        BasePrice = basePrice;
        Discount = discount;
        AvailableCount = availableCount;
        CategoryId = categoryId;
    }

    public static Product Create(ProductId id, string name, string smallDescription, string slug, decimal basePrice, int discount, int availableCount, CategoryId categoryId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (basePrice <= 0) throw new ArgumentOutOfRangeException(nameof(basePrice));
        if (availableCount < 0) throw new ArgumentOutOfRangeException(nameof(availableCount));

        return new Product(id, name, smallDescription, slug, basePrice, discount, availableCount, categoryId);
    }

    public void AddFeature(ProductFeature feature)
    {
        _features.Add(feature);
    }

    public void AddMedia(ProductMedia media)
    {
        _media.Add(media);
    }
    public void UpdateDetails(string name, string smallDescription, string slug, decimal basePrice, int discount, int availableCount, CategoryId categoryId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        if (basePrice <= 0) throw new ArgumentOutOfRangeException(nameof(basePrice));
        if (availableCount < 0) throw new ArgumentOutOfRangeException(nameof(availableCount));

        Name = name;
        SmallDescription = smallDescription;
        Slug = slug;
        BasePrice = basePrice;
        Discount = discount;
        AvailableCount = availableCount;
        CategoryId = categoryId;
    }

}
