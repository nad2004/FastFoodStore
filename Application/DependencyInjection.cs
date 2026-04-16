using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    /// <summary>
    /// Dependency Injection extension methods for Application layer
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures application services including AutoMapper
        /// </summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register AutoMapper
            services.AddAutoMapper(typeof(DependencyInjection));

            // Add application services here as they are created
            // services.AddScoped<IOrderService, OrderService>();
            // services.AddScoped<IMenuItemService, MenuItemService>();

            return services;
        }
    }
}
