using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.DAL.Repositories;
using LiwaPOS.DAL.Services;
using LiwaPOS.Shared.Consts;
using LiwaPOS.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LiwaPOS.DAL
{
    public static class DependencyInjection
    {
        private static string GetConnectionString()
        {
            string connectionString =  ConnectionService.GetConnectionString().Result;

            return string.IsNullOrEmpty(connectionString) ? connectionString : Defaults.DefaultConnectionString;
        }

        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                 options.UseSqlServer(GetConnectionString()));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAppRuleRepository, AppRuleRepository>();
            services.AddScoped<IAppActionRepository, AppActionRepository>();

            return services;
        }
    }
}
