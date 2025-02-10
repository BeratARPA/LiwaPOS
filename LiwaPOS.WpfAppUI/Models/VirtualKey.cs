using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels;

namespace LiwaPOS.WpfAppUI.Models
{
    public enum KeyState
    {
        FirstSet,
        SecondSet
    }

    public class VirtualKey: ViewModelBase
    {  
        private KeyState _keyState;
        public KeyState KeyState
        {
            get => _keyState;
            set
            {
                _keyState = value;
                OnPropertyChanged(nameof(Caption)); // Property adını düzelt
            }
        }

        public string Caption
        {
            get { return KeyState == KeyState.FirstSet ? LowKey : UpKey; }
        }

        public string LowKey { get; set; }
        public string UpKey { get; set; }
        public Keys Key { get; set; }

        public VirtualKey(string lowKey, string upKey, Keys key)
        {
            LowKey = lowKey;
            UpKey = upKey;
            Key = key;
        }

        public VirtualKey(Keys key)
        {
            Key = key;

            try
            {
                LowKey = User32Interop.ToUnicode(key, Keys.None);
            }
            catch (Exception) { LowKey = " "; }

            try
            {
                UpKey = User32Interop.ToUnicode(key, Keys.ShiftKey);
            }
            catch (Exception) { UpKey = " "; }
        }
    }
}
