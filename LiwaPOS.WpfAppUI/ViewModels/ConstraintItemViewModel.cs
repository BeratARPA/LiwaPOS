using System.Collections.ObjectModel;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class ConstraintItemViewModel : ViewModelBase
    {
        public ObservableCollection<string>? DataObject { get; set; }
        public ObservableCollection<string>? Operations { get; set; }
        public string? SelectedValue { get; set; }
        public string? SelectedOperation { get; set; }
        public string? Right { get; set; }
    }
}
