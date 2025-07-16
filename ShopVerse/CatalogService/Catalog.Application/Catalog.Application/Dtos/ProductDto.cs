namespace Catalog.Application.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string SmallDescription { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public decimal BasePrice { get; set; }
        public int Discount { get; set; }
        public int AvailableCount { get; set; }
        public string CategoryId { get; set; } = default!;

        public List<ProductFeatureDto> Features { get; set; } = new();
        public List<ProductMediaDto> Media { get; set; } = new();
    }

    public class ProductFeatureDto
    {
        public string FeatureName { get; set; } = default!;
        public string FeatureValue { get; set; } = default!;
    }

    public class ProductMediaDto
    {
        public string Url { get; set; } = default!;
        public string MediaType { get; set; } = default!;
    }
}
