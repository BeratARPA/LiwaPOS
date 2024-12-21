using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.DAL.Repositories;
using LiwaPOS.DAL.Services;
using LiwaPOS.Entities.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace LiwaPOS.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {      
            services.AddScoped<DataContext>();
            services.AddTransient<DatabaseInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Generic Repository Tanımlamaları
            services.AddScoped<IGenericRepository<Terminal>, GenericRepository<Terminal>>();
            services.AddScoped<IGenericRepository<Script>, GenericRepository<Script>>();
            services.AddScoped<IGenericRepository<AppRule>, GenericRepository<AppRule>>();
            services.AddScoped<IGenericRepository<AppAction>, GenericRepository<AppAction>>();
            services.AddScoped<IGenericRepository<RuleActionMap>, GenericRepository<RuleActionMap>>();
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();

            // Özel Repository Tanımlamaları
            services.AddScoped<ITerminalRepository, TerminalRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IScriptRepository, ScriptRepository>();
            services.AddScoped<IAppRuleRepository, AppRuleRepository>();
            services.AddScoped<IAppActionRepository, AppActionRepository>();
            services.AddScoped<IRuleActionMapRepository, RuleActionMapRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<LocalizationRepository>();

            return services;
        }
    }
}
