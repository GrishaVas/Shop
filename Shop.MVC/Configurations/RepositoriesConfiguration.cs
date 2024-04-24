using Shop.Infrastructure.Abstractions;
using Shop.Infrastructure.Implemintations;

namespace Shop.MVC.Configurations
{
    public static class RepositoriesConfiguration
    {
        public static void AddRepositoriesConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
        }
    }
}
