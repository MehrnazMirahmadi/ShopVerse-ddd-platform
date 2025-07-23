namespace Catalog.Application.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid? ParentId { get; set; }

    public List<CategoryDto> Children { get; set; } = new();
}
