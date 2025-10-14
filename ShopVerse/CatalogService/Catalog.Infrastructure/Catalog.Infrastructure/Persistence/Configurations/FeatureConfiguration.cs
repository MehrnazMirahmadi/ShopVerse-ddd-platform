using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Configurations;

internal class FeatureConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id)
            .HasGuidConversion(FeatureId.Of, id => id.Value)
            .ValueGeneratedNever();

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}
