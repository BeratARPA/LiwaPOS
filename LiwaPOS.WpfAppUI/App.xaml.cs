using DevExpress.Xpf.Core;
using LiwaPOS.BLL;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;
using LiwaPOS.DAL;
using LiwaPOS.Entities;
using LiwaPOS.Shared.Enums;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.Locators;
using LiwaPOS.WpfAppUI.Properties;
using LiwaPOS.WpfAppUI.Services;
using LiwaPOS.WpfAppUI.Themes;
using LiwaPOS.WpfAppUI.UserControls;
using LiwaPOS.WpfAppUI.ViewModels;
using LiwaPOS.WpfAppUI.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LiwaPOS.WpfAppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private ServiceProvider _serviceProvider;

        private void ConfigureServices(IServiceCollection services)
        {
            // Entities servisleri
            services.AddEntitiesLayer();

            // Data Access Layer servisleri
            services.AddDataAccessLayer();

            // Business Logic Layer servisleri
            services.AddBusinessLogicLayer();

            services.AddSingleton<ICustomNotificationService, CustomNotificationService>();
            services.AddSingleton<IApplicationStateService, ApplicationStateService>();
            services.AddSingleton<INavigatorService, NavigatorService>();
            services.AddSingleton<IWebService, WebService>();

            // ViewModels      
            services.AddTransient<AutomationCommandManagementViewModel>();
            services.AddTransient<AutomationCommandsViewModel>();
            services.AddTransient<AppActionManagementViewModel>();
            services.AddTransient<LocalSettingsManagementViewModel>();
            services.AddTransient<AppActionsViewModel>();
            services.AddTransient<AppRuleManagementViewModel>();
            services.AddTransient<AppRulesViewModel>();
            services.AddTransient<ErrorReportViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<NavigationViewModel>();
            services.AddTransient<NotificationViewModel>();
            services.AddTransient<ManagementViewModel>();
            services.AddTransient<ScriptManagementViewModel>();
            services.AddTransient<ScriptsViewModel>();
            services.AddTransient<ShellViewModel>();

            // Views       
            services.AddTransient<AutomationCommandManagementUserControl>();
            services.AddTransient<AutomationCommandsUserControl>();
            services.AddTransient<AppActionManagementUserControl>();
            services.AddTransient<LocalSettingsManagementUserControl>();
            services.AddTransient<AppActionsUserControl>();
            services.AddTransient<AppRuleManagementUserControl>();
            services.AddTransient<AppRulesUserControl>();
            services.AddTransient<LoginUserControl>();
            services.AddTransient<NavigationUserControl>();
            services.AddTransient<ManagementUserControl>();
            services.AddTransient<ScriptManagementUserControl>();
            services.AddTransient<ScriptsUserControl>();
            services.AddTransient<Shell>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            if (Settings.Default.UseDarkMode)
            {
                ThemesController.SetTheme(ThemeType.SoftDark);
                ApplicationThemeHelper.ApplicationThemeName = Theme.Win11DarkName;
            }
            else
            {
                ThemesController.SetTheme(ThemeType.LightTheme);
                ApplicationThemeHelper.ApplicationThemeName = Theme.Win11LightName;
            }

            base.OnStartup(e);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            try
            {
                // DI Container'ı oluştur
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                _serviceProvider = serviceCollection.BuildServiceProvider();
                GlobalVariables.ServiceProvider = _serviceProvider;

                var viewModelLocator = new ViewModelLocator();
                Resources.Add("ViewModelLocator", viewModelLocator);

                // TranslatorExtension'ı başlat
                TranslatorExtension.Initialize(_serviceProvider);

                var appRuleManager = _serviceProvider.GetRequiredService<AppRuleManager>();
                GlobalVariables.Navigator = new NavigatorService(_serviceProvider, appRuleManager);
                // Uygulamayı başlat
                var shell = _serviceProvider.GetRequiredService<Shell>();
                shell.DataContext = _serviceProvider.GetRequiredService<ShellViewModel>();
                shell.Show();

                await appRuleManager.ExecuteAppRulesForEventAsync(EventType.ShellInitialized);

                GlobalVariables.CustomNotificationService = _serviceProvider.GetRequiredService<ICustomNotificationService>();

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private void HandleException(Exception exception)
        {
            if (exception == null) return;

            // ErrorReportWindow ve ViewModel'i çözümleyin
            var errorReportWindow = new ErrorReportWindow();
            var errorReportViewModel = new ErrorReportViewModel(exception);

            // ViewModel'i pencereye bağlayın
            errorReportWindow.DataContext = errorReportViewModel;

            // ErrorReportWindow'u gösterin
            errorReportWindow.ShowDialog();

            // Uygulamayı sonlandırın
            //Environment.Exit(1);
        }
    }
}
