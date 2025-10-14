using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopVerse.IdentityServer.Infrastructure.Identity;

namespace ShopVerse.IdentityServer.Infrastructure.Persistence;

public class ShopVerseIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public ShopVerseIdentityDbContext(DbContextOptions<ShopVerseIdentityDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}
