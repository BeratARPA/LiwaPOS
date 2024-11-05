using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using System.Windows;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class DynamicPropertyEditorViewModel : ViewModelBase
    {
        private object _model;
        public List<UIElement> DynamicInputs { get; private set; }

        public ICommand SaveCommand { get; set; }

        public DynamicPropertyEditorViewModel(object model)
        {
            _model = model;
            DynamicInputs = DynamicInputGenerator.GenerateInputs(_model);

            SaveCommand = new RelayCommand(SavePropery);
        }

        private void SavePropery(object obj)
        {
            UpdateModel(_model);
        }

        public void UpdateModel(dynamic model)
        {
            DynamicInputGenerator.UpdateModelFromInputs(_model, DynamicInputs);
        }
    }
}
