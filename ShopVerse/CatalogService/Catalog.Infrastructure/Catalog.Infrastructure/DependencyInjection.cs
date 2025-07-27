using Catalog.Application.Services;
using Catalog.Domain.Contract;
using Catalog.Domain.Contract.Repositories;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Persistence.Context;
using Catalog.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null
                );
            }));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        var jwtSettings = configuration.GetSection("JwtSettings");
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                         {
                             options.RequireHttpsMetadata = false; // only for local dev
                             options.SaveToken = true;
                             options.TokenValidationParameters = new TokenValidationParameters
                             {
                                 ValidateIssuer = true,
                                 ValidateAudience = true,
                                 ValidateLifetime = true,
                                 ValidateIssuerSigningKey = true,
                                 ValidIssuer = jwtSettings["Issuer"],      // "ShopVerse.Identity"
                                 ValidAudience = jwtSettings["Audience"],  // "ShopVerse.Users"
                                 IssuerSigningKey = new SymmetricSecurityKey(
                                     Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
                             };
                         });
        //  Add Authorization policies
        services.AddAuthorization(options =>
        {
            options.AddPolicy("VerifiedOnly", policy =>
                policy.RequireClaim("IsVerified", "true"));

            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin"));
        });
        return services;
    }
}
