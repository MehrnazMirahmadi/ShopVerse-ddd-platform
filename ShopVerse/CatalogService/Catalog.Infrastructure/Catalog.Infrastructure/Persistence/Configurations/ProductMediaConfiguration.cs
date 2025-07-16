using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Configurations;

internal class ProductMediaConfiguration : IEntityTypeConfiguration<ProductMedia>
{
    public void Configure(EntityTypeBuilder<ProductMedia> builder)
    {
        builder.HasKey(pm => pm.Id);
        builder.Property(pm => pm.Id)
            .HasGuidConversion(ProductMediaId.Of, id => id.Value)
            .ValueGeneratedNever();

        builder.Property(pm => pm.MediaTypeId)
            .HasGuidConversion(ProductMediaTypeId.Of, id => id.Value);

        builder.Property(pm => pm.Url)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(pm => pm.FileSize);
        builder.Property(pm => pm.UploadingUserName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(pm => pm.IsConfirmed);
        builder.Property(pm => pm.ConfirmedByUserName)
            .HasMaxLength(200)
            .IsRequired(false);
    }
}
