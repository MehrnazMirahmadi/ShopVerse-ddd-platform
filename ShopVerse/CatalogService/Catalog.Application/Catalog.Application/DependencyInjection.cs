using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

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
