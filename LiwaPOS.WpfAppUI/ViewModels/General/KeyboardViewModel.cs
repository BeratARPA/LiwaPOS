using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Models;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.General
{
    public class KeyboardViewModel : ViewModelBase
    {
        public VirtualKeyboard Model { get; set; }
        public ICommand PressKeyCommand { get; }

        public KeyboardViewModel()
        {
            Model = new VirtualKeyboard();

            PressKeyCommand = new RelayCommand(OnKeyPress);
        }

        private void OnKeyPress(object obj)
        {
            if (obj is VirtualKey virtualKey)
                Model.ProcessKey(virtualKey.Key);
        }
    }
}
