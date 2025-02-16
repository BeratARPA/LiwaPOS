using LiwaPOS.BLL.ValueChangeSystem.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace LiwaPOS.BLL.ValueChangeSystem
{
    public static class ValueResolutionExtensions
    {
        public static IServiceCollection AddDynamicValueResolution(this IServiceCollection services)
        {
            services.AddSingleton<TokenRegistry>();
            services.AddSingleton<IDynamicValueResolver, TokenExpressionEngine>();

            // Handler'ları kaydet
            services.AddSingleton<ITokenHandler, DateTimeTokenHandler>();
            // Diğer handler'ları buraya ekleyin

            return services;
        }
    }
}
