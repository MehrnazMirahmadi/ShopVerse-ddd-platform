using Finance.Domain.Entities.Invoices;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Persistence.Configurations;


public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);
        builder.Property(i => i.IssuedAt).IsRequired();
        builder.Property(i => i.Status).IsRequired();
        builder.Property(i => i.RelatedEntityType).HasMaxLength(100);

        builder.OwnsMany(i => i.Items, itemBuilder =>
        {
            itemBuilder.WithOwner().HasForeignKey("InvoiceId");
            itemBuilder.Property(i => i.Description).HasMaxLength(200);
            itemBuilder.Property(i => i.UnitPrice).HasPrecision(18, 2);
            itemBuilder.Ignore(i => i.TotalPrice);

            //itemBuilder.Property(i => i.TotalPrice).HasPrecision(18, 2);
            itemBuilder.ToTable("InvoiceItems");
        });

        builder.HasMany(i => i.Payments)
               .WithOne()
               .HasForeignKey("InvoiceId");

        builder.ToTable("Invoices");
    }
}