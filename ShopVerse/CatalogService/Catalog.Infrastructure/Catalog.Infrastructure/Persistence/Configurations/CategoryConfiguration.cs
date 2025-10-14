using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasGuidConversion(CategoryId.Of, id => id.Value)
            .ValueGeneratedNever();

        builder.Property(c => c.ParentId)
            .HasGuidConversion(CategoryId.Of, id => id.Value);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Navigation(c => c.Children).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

