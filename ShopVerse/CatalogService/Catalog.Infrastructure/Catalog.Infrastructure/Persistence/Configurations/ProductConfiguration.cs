using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasGuidConversion(ProductId.Of, id => id.Value)
            .ValueGeneratedNever();

        builder.Property(p => p.CategoryId)
            .HasGuidConversion(CategoryId.Of, id => id.Value);

        builder.Property(p => p.BasePrice)
            .HasPrecision(18, 4);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.SmallDescription)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.Discount);
        builder.Property(p => p.AvailableCount);

        // Optional: Configure backing fields (if needed)
        builder.Navigation(p => p.Features).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(p => p.Media).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}