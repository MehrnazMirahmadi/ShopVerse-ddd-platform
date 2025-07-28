using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopVerse.IdentityServer.Application.Interfaces;
using ShopVerse.IdentityServer.Infrastructure.Identity;
using ShopVerse.IdentityServer.Infrastructure.Persistence;
using ShopVerse.IdentityServer.Infrastructure.Services;

namespace ShopVerse.IdentityServer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityServerInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ShopVerseIdentityDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null
                );
            }));

        services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<ShopVerseIdentityDbContext>()
          .AddDefaultTokenProviders();


        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
