using System.Windows.Controls;

namespace LiwaPOS.WpfAppUI.Models
{
    public class TabContainer
    {
        public string Header { get; set; }
        public bool AllowHide { get; set; }
        public bool IsSelected { get; set; }
        public Frame Content { get; set; } // ViewModelBase'den türeyen içerik veya kullanıcı arayüzü nesneleri
    }
}
