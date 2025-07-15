using Catalog.Domain.ValueObjects;
using ShopVerse.BuildingBlocks.Abstractions;

namespace Catalog.Domain.Entities;

public class Category : Aggregate<CategoryId>
{
    public string Name { get; private set; }
    public CategoryId? ParentId { get; private set; }

    private readonly List<Category> _children = new();
    public IReadOnlyCollection<Category> Children => _children.AsReadOnly();

    private Category(CategoryId id, string name, CategoryId? parentId)
    {
        Id = id;
        Name = name;
        ParentId = parentId;
    }

    public static Category Create(CategoryId id, string name, CategoryId? parentId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        if (parentId != null && id == parentId)
            throw new ArgumentException("ParentId cannot be the same as the Category Id");

        return new Category(id, name, parentId);
    }

    public void AddChildCategory(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        if (category.ParentId != this.Id)
            throw new InvalidOperationException("Child category's ParentId must be set to this category's Id");

        _children.Add(category);
    }
}
