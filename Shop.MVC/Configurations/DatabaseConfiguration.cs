using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;

namespace Shop.MVC.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ShopDbContext>(opts =>
            {
                opts.UseNpgsql(config.GetConnectionString("PostgreSQL"));
            });
        }
    }
}
