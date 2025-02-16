using LiwaPOS.WpfAppUI.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.General
{
    public class DynamicSelectionViewModel<T> : ViewModelBase
    {
        public string Title { get; set; }
        private ObservableCollection<T> _allItems;
        public ObservableCollection<T> SelectedItems { get; set; }
        public T SelectedAvailableItem { get; set; }

        public ICommand AddSelectedCommand { get; }
        public ICommand RemoveSelectedCommand { get; }
        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        private Action<ObservableCollection<T>> _onConfirmAction;
        private ICollectionView _availableItemsView;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    _availableItemsView.Refresh();
                }
            }
        }

        public ICollectionView AvailableItemsView => _availableItemsView;

        public DynamicSelectionViewModel(ObservableCollection<T> availableItems,
                                         ObservableCollection<T> selectedItems,
                                         Action<ObservableCollection<T>> onConfirmAction)
        {
            SelectedItems = selectedItems;
            _allItems = new ObservableCollection<T>(availableItems);
            _onConfirmAction = onConfirmAction;

            AddSelectedCommand = new RelayCommand(AddSelected);
            RemoveSelectedCommand = new RelayCommand(RemoveSelected);
            ConfirmCommand = new RelayCommand(ConfirmSelection);
            CancelCommand = new RelayCommand(Cancel);

            _availableItemsView = CollectionViewSource.GetDefaultView(_allItems);
            _availableItemsView.Filter = FilterAvailableItems;
        }

        private void Cancel(object obj)
        {
            _onConfirmAction?.Invoke(SelectedItems);
        }

        private void AddSelected(object obj)
        {
            // Seçilen öğe boş değilse ve sağ listede değilse ekleyin
            if (SelectedAvailableItem != null && !SelectedItems.Contains(SelectedAvailableItem))
            {
                SelectedItems.Add(SelectedAvailableItem);

                // Eğer mevcut listede varsa, soldaki listeden kaldırın
                if (_allItems.Contains(SelectedAvailableItem))
                {
                    _allItems.Remove(SelectedAvailableItem);
                }

                // Görünümü güncelleyin
                _availableItemsView.Refresh();
            }
        }

        private void RemoveSelected(object obj)
        {
            // Seçili öğe sağ listede varsa işlem yapın
            if (SelectedAvailableItem != null && SelectedItems.Contains(SelectedAvailableItem))
            {
                // Mevcut listede değilse, öğeyi sol listeye ekleyin
                if (!_allItems.Contains(SelectedAvailableItem))
                {
                    _allItems.Add(SelectedAvailableItem);
                }

                // Sağ listeden öğeyi kaldırın
                SelectedItems.Remove(SelectedAvailableItem);

                // Görünümü güncelleyin
                _availableItemsView.Refresh();
            }
        }

        private bool FilterAvailableItems(object item)
        {
            if (item is T typedItem)
            {
                // Eğer sağ taraftaki seçili öğelerde varsa false döndür
                if (SelectedItems.Contains(typedItem))
                    return false;

                // Eğer item içinde Name özelliği varsa, ona göre filtrele
                var itemName = typedItem.GetType().GetProperty("Name")?.GetValue(typedItem)?.ToString();
                return string.IsNullOrWhiteSpace(SearchText) ||
                       itemName != null && itemName.IndexOf(SearchText, StringComparison.InvariantCultureIgnoreCase) >= 0;
            }

            return false;
        }

        private void ConfirmSelection(object obj)
        {
            _onConfirmAction?.Invoke(SelectedItems);
        }

        public async Task LoadItemsAsync(Func<Task<ObservableCollection<T>>> loadDataFunc)
        {
            var loadedItems = await loadDataFunc();

            _allItems.Clear();
            foreach (var item in loadedItems)
            {
                var itemGuid = item?.GetType().GetProperty("EntityGuid")?.GetValue(item) as Guid?;

                // SelectedItems listesindeki herhangi bir öğenin aynı EntityGuid'e sahip olup olmadığını kontrol ediyoruz
                bool isInSelectedItems = SelectedItems.Any(selectedItem =>
                {
                    var selectedItemGuid = selectedItem?.GetType().GetProperty("EntityGuid")?.GetValue(selectedItem) as Guid?;
                    return itemGuid == selectedItemGuid;
                });

                // Eğer item, SelectedItems içinde yoksa _allItems listesine ekliyoruz
                if (!isInSelectedItems)
                {
                    _allItems.Add(item);
                }
            }

            _availableItemsView.Refresh();
        }
    }
}
