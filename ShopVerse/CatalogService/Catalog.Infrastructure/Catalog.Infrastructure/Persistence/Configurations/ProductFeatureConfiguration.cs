using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Configurations;

internal class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
{
    public void Configure(EntityTypeBuilder<ProductFeature> builder)
    {
        builder.HasKey(pf => pf.Id);
        builder.Property(pf => pf.Id)
            .HasGuidConversion(ProductFeatureId.Of, id => id.Value)

            .ValueGeneratedNever();

        builder.Property(pf => pf.FeatureId)
            .HasGuidConversion(FeatureId.Of, id => id.Value);

        builder.Property(pf => pf.FeatureValue)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(pf => pf.EffectOnPrice);
        builder.Property(pf => pf.IsDefault);
    }
}

