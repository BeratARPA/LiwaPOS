using LiwaPOS.BLL;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL;
using LiwaPOS.Entities;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Services;
using LiwaPOS.WpfAppUI.UserControls;
using LiwaPOS.WpfAppUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LiwaPOS.WpfAppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private void ConfigureServices(IServiceCollection services)
        {
            //Entities servisleri
            services.AddEntitiesLayer();

            // Data Access Layer servisleri
            services.AddDataAccessLayer();

            // Business Logic Layer servisleri
            services.AddBusinessLogicLayer();

            services.AddSingleton<ICustomNotificationService, CustomNotificationService>();

            // ViewModels
            services.AddTransient<NavigationViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<ShellViewModel>();
           
            // Views
            services.AddTransient<LoginUserControl>();
            services.AddTransient<NavigationUserControl>();
            services.AddTransient<Shell>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // DI Container'ı oluştur
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // TranslatorExtension'ı başlat
            TranslatorExtension.Initialize(serviceProvider);

            // Uygulamayı başlat
            var shell = serviceProvider.GetRequiredService<Shell>();
            shell.DataContext = serviceProvider.GetRequiredService<ShellViewModel>();
            shell.Show();
        }
    }
}
