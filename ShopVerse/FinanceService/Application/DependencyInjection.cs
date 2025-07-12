using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ShopVerse.BuildingBlocks.Messaging.MassTransit;
using Finance.Application.Finance.EventHandlers.Integration;

namespace Finance.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        // services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        //services.AddMessageBroker(configuration, "finance", Assembly.GetExecutingAssembly());
        services.AddMessageBroker(configuration, "finance", typeof(OrderCheckoutEventHandler).Assembly);

        return services;
    }
}
