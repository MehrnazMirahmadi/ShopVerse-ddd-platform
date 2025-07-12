using ShopVerse.BuildingBlocks.Exeptions.Handler;

namespace Finance.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddExceptionHandler<CustomExceptionHandler>();
            return services;
        }
    }
}
