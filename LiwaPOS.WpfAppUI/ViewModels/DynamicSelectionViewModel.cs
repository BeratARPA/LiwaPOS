using LiwaPOS.WpfAppUI.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class DynamicSelectionViewModel<T> : ViewModelBase
    {
        public string Title { get; set; }
        private ObservableCollection<T> _allItems { get; set; }
        public ObservableCollection<T> AvailableItems { get; set; }
        public ObservableCollection<T> SelectedItems { get; set; }
        public T SelectedAvailableItem { get; set; }

        public ICommand AddSelectedCommand { get; }
        public ICommand RemoveSelectedCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        private Action<ObservableCollection<T>> _onConfirmAction;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                Search(_searchText); // Arama fonksiyonu metin değiştiğinde tetiklenir.
            }
        }

        public DynamicSelectionViewModel(ObservableCollection<T> availableItems,
                                         ObservableCollection<T> selectedItems,
                                         Action<ObservableCollection<T>> onConfirmAction)
        {
            AvailableItems = availableItems;
            _allItems = new ObservableCollection<T>(availableItems);
            SelectedItems = selectedItems;
            _onConfirmAction = onConfirmAction;

            AddSelectedCommand = new RelayCommand(AddSelected);
            RemoveSelectedCommand = new RelayCommand(RemoveSelected);
            SearchCommand = new RelayCommand(obj => Search((string)obj));
            ConfirmCommand = new RelayCommand(ConfirmSelection);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Cancel(object obj)
        {
            SelectedItems.Clear();
        }

        private void AddSelected(object obj)
        {
            if (SelectedAvailableItem != null && !SelectedItems.Contains(SelectedAvailableItem))
            {
                SelectedItems.Add(SelectedAvailableItem);
            }
        }

        private void RemoveSelected(object obj)
        {
            if (SelectedItems.Contains(SelectedAvailableItem))
            {
                SelectedItems.Remove(SelectedAvailableItem);
            }
        }

        private void Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Eğer arama metni boşsa, tüm öğeleri göster
                AvailableItems.Clear();
                foreach (var item in _allItems)
                {
                    AvailableItems.Add(item);
                }
            }
            else
            {
                // Eğer arama metni varsa, filtrele
                var filteredItems = _allItems.Where(i => i.ToString().Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList();
                AvailableItems.Clear();
                foreach (var item in filteredItems)
                {
                    AvailableItems.Add(item);
                }
            }
        }

        private void ConfirmSelection(object obj)
        {
            _onConfirmAction?.Invoke(SelectedItems);
        }
    }
}
