using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class NavigationViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
