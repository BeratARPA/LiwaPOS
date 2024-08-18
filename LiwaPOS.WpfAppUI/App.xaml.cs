using LiwaPOS.BLL;
using LiwaPOS.DAL;
using LiwaPOS.Entities;
using LiwaPOS.WpfAppUI.UserControls;
using LiwaPOS.WpfAppUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LiwaPOS.WpfAppUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //Entities servisleri
            services.AddEntitiesLayer();

            // Data Access Layer servisleri
            services.AddDataAccessLayer();

            // Business Logic Layer servisleri
            services.AddBusinessLogicLayer();

            // ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<ShellViewModel>();

            // Views
            services.AddTransient<LoginUserControl>();
            services.AddTransient<Shell>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var shell = _serviceProvider.GetRequiredService<Shell>();
            shell.DataContext = _serviceProvider.GetRequiredService<ShellViewModel>();
            shell.Show();
            base.OnStartup(e);
        }
    }
}
