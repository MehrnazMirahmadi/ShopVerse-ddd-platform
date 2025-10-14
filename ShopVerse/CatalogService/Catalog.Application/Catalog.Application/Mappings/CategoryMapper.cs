using Catalog.Application.Dtos;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Mappings;

public static class CategoryMapper
{
    public static CategoryDto ToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id.Value,
            Name = category.Name,
            ParentId = category.ParentId?.Value
        };
    }
    public static Category FromDto(CategoryDto dto)
    {
        var categoryId = CategoryId.Of(dto.Id);
        CategoryId? parentId = dto.ParentId.HasValue ? CategoryId.Of(dto.ParentId.Value) : null;
        return Category.Create(categoryId, dto.Name, parentId);
    }
}
