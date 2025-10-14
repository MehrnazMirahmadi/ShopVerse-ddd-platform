using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasConversion(
                    id => id.Value,
                    value => OrderItemId.Of(value))
               .ValueGeneratedNever();

        builder.Property(x => x.OrderId)
               .HasConversion(
                    id => id.Value,
                    value => OrderId.Of(value))
               .IsRequired();

        builder.Property(x => x.ProductId)
               .HasConversion(
                    id => id.Value,
                    value => ProductId.Of(value))
               .IsRequired();

        builder.Property(x => x.Quantity)
               .IsRequired();

        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(p => p.Amount)
                 .HasColumnName("Price_Amount")
                 .IsRequired();

            price.Property(p => p.Currency)
                 .HasColumnName("Price_Currency")
                 .HasMaxLength(10)
                 .IsRequired();
        });

    }
}