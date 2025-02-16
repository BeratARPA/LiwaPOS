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
            services.AddScoped<DataContext>();
            services.AddTransient<DatabaseInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Generic Repository Tanımlamaları
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Özel Repository Tanımlamaları
            services.AddScoped<IRelationshipChecker, RelationshipChecker>();
            services.AddScoped<IPrinterTemplateRepository, PrinterTemplateRepository>();
            services.AddScoped<IPrinterRepository, PrinterRepository>();
            services.AddScoped<ITerminalRepository, TerminalRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IScriptRepository, ScriptRepository>();
            services.AddScoped<IAppRuleRepository, AppRuleRepository>();
            services.AddScoped<IAppActionRepository, AppActionRepository>();
            services.AddScoped<IRuleActionMapRepository, RuleActionMapRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IProgramSettingValueRepository, ProgramSettingValueRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IActionContainerRepository, ActionContainerRepository>();

            services.AddSingleton<LocalizationRepository>();

            return services;
        }
    }
}
