using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => InventoryItemId.Of(value)
            )
            .ValueGeneratedNever();
        builder.Property(x => x.ProductId)
            .HasConversion(
                id => id.Value,
                value => ProductId.Of(value)
            )
            .IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
    }
}

