using LiwaPOS.BLL.Actions;
using LiwaPOS.BLL.EventHandlers;
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserManager>();

            services.AddScoped<IAppRuleService, AppRuleService>();
            services.AddScoped<AppRuleManager>();

            services.AddScoped<IAppActionService, AppActionService>();

            services.AddScoped<IRuleActionMapService, RuleActionMapService>();

            services.AddScoped<IScriptService, ScriptService>();

            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IGoogleMapService, GoogleMapService>();
            services.AddSingleton<ISmsService, TelsamSmsService>();

            services.AddSingleton<EventFactory>();
            services.AddSingleton<ActionFactory>();
            services.AddSingleton<LocalizationService>();
            services.AddSingleton<JavaScriptEngineService>();

            services.AddTransient<CloseTheApplicationAction>();
            services.AddTransient<LoginUserAction>();
            services.AddTransient<LogoutUserAction>();
            services.AddTransient<OpenPageAction>();
            services.AddTransient<ShowPopupAction>();
            services.AddTransient<SendEmailAction>();
            services.AddTransient<TelsamSendSmsAction>();
            services.AddTransient<RunProcessAction>();
            services.AddTransient<AddLineToTextFileAction>();
            services.AddTransient<OpenWebsiteOnWindowAction>();
            services.AddTransient<ShowGoogleMapsDirectionsAction>();
            services.AddTransient<RunScriptAction>();
            services.AddTransient<WaitAction>();

            services.AddTransient<POSPageOpenedEventHandler>();
            services.AddTransient<UserLoggedInEventHandler>();
            services.AddTransient<PopupDisplayedEventHandler>();
            services.AddTransient<UserFailedToLoginEventHandler>();
            services.AddTransient<ShellInitializedEventHandler>();

            return services;
        }
    }
}
