using LiwaPOS.BLL;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;
using LiwaPOS.DAL;
using LiwaPOS.Entities;
using LiwaPOS.Shared.Enums;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Interfaces;
using LiwaPOS.WpfAppUI.Services;
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
            //Entities servisleri
            services.AddEntitiesLayer();

            // Data Access Layer servisleri
            services.AddDataAccessLayer();

            // Business Logic Layer servisleri
            services.AddBusinessLogicLayer();

            services.AddSingleton<ICustomNotificationService, CustomNotificationService>();
            services.AddSingleton<IApplicationStateService, ApplicationStateService>();
            services.AddSingleton<IWebService, WebService>();

            // ViewModels
            services.AddTransient<NavigationViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<ScriptsViewModel>();
            services.AddTransient<ScriptManagementViewModel>();
           

            // Views
            services.AddTransient<LoginUserControl>();
            services.AddTransient<NavigationUserControl>();
            services.AddTransient<ManagementUserControl>();
            services.AddTransient<ScriptManagementUserControl>();
            services.AddTransient<ScriptsUserControl>();
            services.AddTransient<Shell>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; ;
            try
            {
                // DI Container'ı oluştur
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                _serviceProvider = serviceCollection.BuildServiceProvider();

                // TranslatorExtension'ı başlat
                TranslatorExtension.Initialize(_serviceProvider);

                // Uygulamayı başlat
                var shell = _serviceProvider.GetRequiredService<Shell>();
                shell.DataContext = _serviceProvider.GetRequiredService<ShellViewModel>();
                shell.Show();

                var appRuleManager = _serviceProvider.GetRequiredService<AppRuleManager>();
                await appRuleManager.ExecuteAppRulesForEventAsync(EventType.ShellInitialized);
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
            Environment.Exit(1);
        }
    }
}
