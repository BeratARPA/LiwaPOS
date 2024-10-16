using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class ViewModelBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Dinamik parametreleri ayarlamak için kullanılacak metod
        public virtual void SetParameter(dynamic parameter)
        {
            // Her ViewModel kendi özel ihtiyaçlarına göre bu metodu geçersiz kılabilir
        }
    }
}
