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
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<UserManager>();

            services.AddSingleton<IAppRuleService, AppRuleService>();
            services.AddSingleton<AppRuleManager>();

            services.AddSingleton<IAppActionService, AppActionService>();

            services.AddSingleton<IRuleActionMapService, RuleActionMapService>();

            services.AddSingleton<IScriptService, ScriptService>();

            services.AddSingleton<IEmailService, EmailService>();

            services.AddSingleton<IGoogleMapService, GoogleMapService>();

            services.AddSingleton<ISmsService, TelsamSmsService>();

            services.AddSingleton<EventFactory>();
            services.AddSingleton<ActionFactory>();
            services.AddSingleton<LocalizationService>();

            services.AddTransient<CloseTheApplicationAction>();
            services.AddTransient<LoginUserAction>();
            services.AddTransient<OpenPOSPageAction>();
            services.AddTransient<ShowPopupAction>();
            services.AddTransient<SendEmailAction>();
            services.AddTransient<TelsamSendSmsAction>();
            services.AddTransient<RunProcessAction>();
            services.AddTransient<AddLineToTextFileAction>();
            services.AddTransient<OpenWebsiteOnWindowAction>();
            services.AddTransient<ShowGoogleMapsDirectionsAction>();

            services.AddTransient<POSPageOpenedEventHandler>();
            services.AddTransient<UserLoggedInEventHandler>();
            services.AddTransient<PopupDisplayedEventHandler>();
            services.AddTransient<UserFailedToLoginEventHandler>();
            services.AddTransient<ShellInitializedEventHandler>();

            return services;
        }
    }
}
