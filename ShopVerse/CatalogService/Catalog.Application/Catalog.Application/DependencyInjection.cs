using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopVerse.BuildingBlocks.Behaviors;

namespace Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });


        // ✅ Register AutoMapper manually 
        services.AddSingleton<IMapper>(sp =>
        {
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(DependencyInjection).Assembly);
              
            }, loggerFactory);

            return config.CreateMapper();
        });

        return services;
    }
}
