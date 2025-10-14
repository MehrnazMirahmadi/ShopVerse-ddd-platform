using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Configurations;

internal class ProductMediaTypeConfiguration : IEntityTypeConfiguration<ProductMediaType>
{
    public void Configure(EntityTypeBuilder<ProductMediaType> builder)
    {
        builder.HasKey(pmt => pmt.Id);
        builder.Property(pmt => pmt.Id)
            .HasGuidConversion(ProductMediaTypeId.Of, id => id.Value)
            .ValueGeneratedNever();

        builder.Property(pmt => pmt.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Navigation(pmt => pmt.Medias).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}