using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopVerse.IdentityServer.Infrastructure.Persistence;

namespace ShopVerse.IdentityServer.Infrastructure.Identity;

public class ShopVerseIdentityDbContextFactory : IDesignTimeDbContextFactory<ShopVerseIdentityDbContext>
{
    public ShopVerseIdentityDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ShopVerseIdentityDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"));

        return new ShopVerseIdentityDbContext(optionsBuilder.Options);
    }
}
