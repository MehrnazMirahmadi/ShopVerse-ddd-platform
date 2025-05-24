using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ShopVerse.BuildingBlocks.Messaging.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker
      (this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq((context, cfg) =>
            {
                var host = configuration["MessageBroker:Host"];
                var username = configuration["MessageBroker:UserName"];
                var password = configuration["MessageBroker:Password"];

                if (string.IsNullOrWhiteSpace(host) ||
                    string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password))
                {
                    throw new InvalidOperationException("RabbitMQ configuration values are missing.");
                }

                cfg.Host(new Uri(host), h =>
                {
                    h.Username(username);
                    h.Password(password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
