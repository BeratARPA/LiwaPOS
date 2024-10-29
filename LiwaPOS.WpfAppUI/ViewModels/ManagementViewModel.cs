using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class ManagementViewModel : ViewModelBase
    {
        public ObservableCollection<TabContainer> TabItems { get; set; }

        // Commands
        public ICommand OpenScriptsCommand { get; }
        public ICommand OpenAppActionsCommand { get; }
        public ICommand OpenAppRulesCommand { get; }
        public ICommand OpenAutomationCommandsCommand { get; }
        public ICommand CloseTabCommand { get; }

        // Selected tab
        private TabContainer _selectedTabItem;
        public TabContainer SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                _selectedTabItem = value;
                OnPropertyChanged(nameof(SelectedTabItem));
                SetFrame(_selectedTabItem?.Content);
            }
        }

        public ManagementViewModel()
        {
            TabItems = new ObservableCollection<TabContainer>();

            // Command initializations
            OpenScriptsCommand = new RelayCommand(OpenScripts);
            OpenAppActionsCommand = new RelayCommand(OpenAppActions);
            OpenAppRulesCommand = new RelayCommand(OpenAppRules);
            OpenAutomationCommandsCommand = new RelayCommand(OpenAutomationCommands);
            CloseTabCommand = new RelayCommand(CloseTab);
        }

        private void CloseTab(object obj)
        {
            if (obj is TabContainer tab && TabItems.Contains(tab))
            {
                TabItems.Remove(tab);
            }
        }

        private void SetFrame(Frame frame)
        {
            GlobalVariables.Navigator.SetFrame(frame);
        }

        private void AddNewTab(string header, string navigationKey)
        {
            var existingTab = TabItems.FirstOrDefault(tab => tab.Header == header);

            if (existingTab != null)
            {
                SetFrame(existingTab.Content);
                SelectedTabItem = existingTab;
                return;
            }

            var frame = new Frame
            {
                NavigationUIVisibility = NavigationUIVisibility.Hidden
            };

            SetFrame(frame);
            GlobalVariables.Navigator.Navigate(navigationKey);

            var newTab = new TabContainer
            {
                Header = header,
                Content = frame,
                IsSelected = true,
                AllowHide = true
            };

            TabItems.Add(newTab);
            SelectedTabItem = newTab;
        }

        private void OpenScripts(object obj) => AddNewTab("Scripts", "Scripts");

        private void OpenAppActions(object obj) => AddNewTab("App Actions", "AppActions");

        private void OpenAppRules(object obj) => AddNewTab("App Rules", "AppRules");

        private void OpenAutomationCommands(object obj) => AddNewTab("Automation Commands", "AutomationCommands");
    }
}
