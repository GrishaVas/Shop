using Shop.Services.Abstractions;
using Shop.Services.Implemintations;

namespace Shop.MVC.Configurations
{
    public static class ServicesConfiguration
    {
        public static void AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
