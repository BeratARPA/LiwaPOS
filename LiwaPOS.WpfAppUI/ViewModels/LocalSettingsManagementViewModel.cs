using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;
using LiwaPOS.Shared.Services;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class LocalSettingsManagementViewModel : ViewModelBase
    {
        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                OnPropertyChanged();
            }
        }

        private bool _useDarkMode;
        public bool UseDarkMode
        {
            get => _useDarkMode;
            set
            {
                _useDarkMode = value;
                OnPropertyChanged();
            }
        }

        private bool _useCustomNavigation;
        public bool UseCustomNavigation
        {
            get => _useCustomNavigation;
            set
            {
                _useCustomNavigation = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LanguageType> Languages { get; }
        private LanguageType _language;
        public LanguageType Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand OpenDynamicProperyEditorWindowCommand { get; }

        public LocalSettingsManagementViewModel()
        {
            Language = LanguageTypeExtension.ToShortString(Properties.Settings.Default.CurrentLanguage);
            Languages = new ObservableCollection<LanguageType>(Enum.GetValues(typeof(LanguageType)).Cast<LanguageType>());
            ConnectionString = ConnectionService.GetConnectionString();

            UseDarkMode = Properties.Settings.Default.UseDarkMode;
            UseCustomNavigation = Properties.Settings.Default.UseCustomNavigation;

            CloseCommand = new RelayCommand(ClosePage);
            SaveCommand = new AsyncRelayCommand(SaveScript);
            OpenDynamicProperyEditorWindowCommand = new RelayCommand(OpenDynamicProperyEditorWindow);
        }

        public void SetParameter(dynamic parameter)
        {

        }

        private void ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("AppActions");
        }

        private async Task SaveScript(object obj)
        {
            Properties.Settings.Default.CurrentLanguage = LanguageTypeExtension.ToShortString(Language);
            await ConnectionService.SaveConnectionString(ConnectionString);
            Properties.Settings.Default.UseDarkMode = UseDarkMode;
            Properties.Settings.Default.UseCustomNavigation = UseCustomNavigation;
            Properties.Settings.Default.Save();
        }

        private void OpenDynamicProperyEditorWindow(object arg)
        {
            var connectionString = new ConnectionStringDTO();
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                connectionString.DataSource = RegexHelper.FindAllMatches(ConnectionString, "Data Source=(.*?);")[0];
                connectionString.UserId = RegexHelper.FindAllMatches(ConnectionString, "User Id=(.*?);")[0];
                connectionString.Password = RegexHelper.FindAllMatches(ConnectionString, "Password=(.*?);")[0];
                connectionString.Database = RegexHelper.FindAllMatches(ConnectionString, "Database=(.*?);")[0];
            }

            var viewModel = new DynamicPropertyEditorViewModel(connectionString);
            viewModel.UpdateModel(connectionString);

            var window = new DynamicPropertyEditorWindow { DataContext = viewModel };
            window.ShowDialog();

            if ((bool)window.DialogResult)
                ConnectionString = ConnectionService.ModelToString(connectionString);
        }
    }
}
