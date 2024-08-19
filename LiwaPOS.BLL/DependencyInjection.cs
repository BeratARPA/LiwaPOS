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
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<UserManager>();

            services.AddSingleton<IAppRuleService, AppRuleService>();
            services.AddSingleton<AppRuleManager>();

            services.AddSingleton<IAppActionService, AppActionService>();

            services.AddSingleton<EventFactory>();
            services.AddSingleton<ActionFactory>();
            services.AddSingleton<LocalizationService>();

            return services;
        }
    }
}
