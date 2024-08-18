using LiwaPOS.BLL.Factories;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;
using LiwaPOS.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LiwaPOS.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            // Servisler ve yöneticiler burada tanımlanır
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserManager>();

            services.AddScoped<IAppRuleService, AppRuleService>();
            services.AddScoped<AppRuleManager>();

            services.AddScoped<IAppActionService, AppActionService>();

            services.AddSingleton<EventFactory>();
            services.AddSingleton<ActionFactory>();

            return services;
        }
    }
}
