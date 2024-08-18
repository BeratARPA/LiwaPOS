using LiwaPOS.Entities.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace LiwaPOS.Entities
{
    public static class DependencyInjection
    {    
        public static IServiceCollection AddEntitiesLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));          

            return services;
        }
    }
}
