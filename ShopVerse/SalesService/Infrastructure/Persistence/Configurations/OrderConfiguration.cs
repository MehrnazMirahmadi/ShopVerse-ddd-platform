using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);


        builder.Property(o => o.Id)
       .HasConversion(
           id => id.Value,
           value => OrderId.Of(value))
       .ValueGeneratedNever();

        // CustomerId mapping
        builder.Property(o => o.CustomerId)
        .HasConversion(
            id => id.Value,
            value => CustomerId.Of(value));


        // OrderName as value object
        builder.OwnsOne(o => o.OrderName, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("OrderName")
                .IsRequired()
                .HasMaxLength(256);
        });

        builder.OwnsOne(o => o.ShippingAddress, sa =>
        {
            sa.Property(p => p.FirstName).HasMaxLength(100);
            sa.Property(p => p.LastName).HasMaxLength(100);
            sa.Property(p => p.EmailAddress).HasMaxLength(100);
            sa.Property(p => p.AddressLine).HasMaxLength(200);
            sa.Property(p => p.Country).HasMaxLength(100);
            sa.Property(p => p.State).HasMaxLength(100);
            sa.Property(p => p.ZipCode).HasMaxLength(20);
        });

        builder.OwnsOne(o => o.BillingAddress, ba =>
        {
            ba.Property(p => p.FirstName).HasMaxLength(100);
            ba.Property(p => p.LastName).HasMaxLength(100);
            ba.Property(p => p.EmailAddress).HasMaxLength(100);
            ba.Property(p => p.AddressLine).HasMaxLength(200);
            ba.Property(p => p.Country).HasMaxLength(100);
            ba.Property(p => p.State).HasMaxLength(100);
            ba.Property(p => p.ZipCode).HasMaxLength(20);
        });


        builder.OwnsOne(o => o.Payment, p =>
        {
            p.Property(x => x.CardName)
                .HasMaxLength(100)
                .IsRequired();

            p.Property(x => x.CardNumber)
                .HasMaxLength(20)
                .IsRequired();

            p.Property(x => x.Expiration)
                .HasMaxLength(10)
                .IsRequired();

            p.Property(x => x.CVV)
                .HasMaxLength(3)
                .IsRequired();

            p.Property(x => x.PaymentMethod)
                .IsRequired();
        });


        // Enum
        builder.Property(o => o.Status)
               .HasConversion<string>()
               .HasMaxLength(50)
               .IsRequired();

        // OrderItems
        builder.HasMany(o => o.OrderItems)
               .WithOne()
               .HasForeignKey(oi => oi.OrderId);


    }
}
