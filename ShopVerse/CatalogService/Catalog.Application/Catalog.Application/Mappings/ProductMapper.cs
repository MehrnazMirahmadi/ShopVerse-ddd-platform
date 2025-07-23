using Catalog.Domain.Entities;
using Catalog.Application.Dtos;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Mappings;



public static class ProductMapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id.Value.ToString(),
            Name = product.Name,
            SmallDescription = product.SmallDescription,
            Slug = product.Slug,
            BasePrice = product.BasePrice,
            Discount = product.Discount,
            AvailableCount = product.AvailableCount,
            CategoryId = product.CategoryId.Value.ToString(),

            Features = product.Features.Select(f => new ProductFeatureDto
            {
                FeatureName = f.FeatureValue,  
                FeatureValue = f.FeatureValue
            }).ToList(),

            Media = product.Media.Select(m => new ProductMediaDto
            {
                Url = m.Url,
                MediaType = m.GetType().Name,
            }).ToList()
        };
    }
    public static Product FromDto(ProductDto dto)
    {
        var product = Product.Create(
            ProductId.Of(Guid.Parse(dto.Id)),
            dto.Name,
            dto.SmallDescription,
            dto.Slug,
            dto.BasePrice,
            dto.Discount,
            dto.AvailableCount,
            CategoryId.Of(Guid.Parse(dto.CategoryId))
        );

        foreach (var featureDto in dto.Features)
        {
            var feature = ProductFeature.Create(
                ProductFeatureId.NewId(),
                FeatureId.NewId(),  // یا مقدار مناسب
                featureDto.FeatureValue,
                0,
                false
            );
            product.AddFeature(feature);
        }

        foreach (var mediaDto in dto.Media)
        {
            var media = ProductMedia.Create(
                ProductMediaId.NewId(),
                ProductMediaTypeId.NewId(),
                mediaDto.Url,
                0,
                "UploaderName"
            );
            product.AddMedia(media);
        }

        return product;
    }

}
