namespace Catalog.Application.Dtos;

public class UpdateProductDto
{
    public string Name { get; set; } = default!;
    public string SmallDescription { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public decimal BasePrice { get; set; }
    public int Discount { get; set; }
    public int AvailableCount { get; set; }
    public string CategoryId { get; set; } = default!;
    public int InventoryCount {  get; set; }
    public List<ProductFeatureDto> Features { get; set; } = new();
    public List<ProductMediaDto> Media { get; set; } = new();
}
