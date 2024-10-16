using Microsoft.Extensions.DependencyInjection;

namespace LiwaPOS.WpfAppUI.Factories
{
    public class ViewModelFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T CreateViewModel<T>() where T : class
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
