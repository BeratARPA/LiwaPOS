using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.DAL.Repositories;
using LiwaPOS.DAL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LiwaPOS.DAL
{
    public static class DependencyInjection
    {    
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {    
            services.AddSingleton<DataContext>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IAppRuleRepository, AppRuleRepository>();
            services.AddSingleton<IAppActionRepository, AppActionRepository>();
            services.AddSingleton<IRuleActionMapRepository, RuleActionMapRepository>();
            services.AddSingleton<LocalizationRepository>();

            return services;
        }
    }
}
