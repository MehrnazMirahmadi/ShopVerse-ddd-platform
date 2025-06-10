using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        
        services.AddDbContext<InventoryDbContext>(options =>
          options.UseSqlServer(connectionString, opt =>
        opt.EnableRetryOnFailure())
                );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
        return services;
    }
}
