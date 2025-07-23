using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
       builder.HasData(
            new Role("Admin") {  Id = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new Role("User") { Id = Guid.Parse("00000000-0000-0000-0000-000000000002") }
        );
    }
}
