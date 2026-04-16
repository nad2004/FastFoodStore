using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    /// <summary>
    /// Dependency Injection extension methods for Infrastructure layer
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures infrastructure services including DbContext
        /// </summary>
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Add repositories here as they are created
            // services.AddScoped<IOrderRepository, OrderRepository>();
            // services.AddScoped<IMenuItemRepository, MenuItemRepository>();

            return services;
        }
    }
}
