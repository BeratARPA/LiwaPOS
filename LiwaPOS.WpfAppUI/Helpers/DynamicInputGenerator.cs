using DevExpress.XtraExport.Xls;
using LiwaPOS.WpfAppUI.Extensions;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace LiwaPOS.WpfAppUI.Helpers
{
    public class DynamicInputGenerator
    {
        public static double MaxLabelWidth(object model)
        {
            if (model == null)
                return 0;

            double maxLabelWidth = 0;
            var properties = model.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var label = new TextBlock
                {
                    Text = prop.Name + ":",
                    FontWeight = FontWeights.Normal,
                    Margin = new Thickness(0, 5, 10, 5)
                };

                // Boyut ölçümünü yap
                label.Measure(new System.Windows.Size(Double.PositiveInfinity, Double.PositiveInfinity));
                maxLabelWidth = Math.Max(maxLabelWidth, label.DesiredSize.Width); // En büyük genişliği bul
            }

            return maxLabelWidth;
        }

        public static List<UIElement> GenerateInputs(object model)
        {
            var inputs = new List<UIElement>();

            if (model == null)
                return inputs;

            // "Parametreler:" başlıklı TextBlock ekle
            var paramLabel = new TextBlock
            {
                Text = TranslatorExtension.TranslateUI("Parameters", ":").Result,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 10, 0, 10),
                FontSize = 16 // Responsive için büyükçe bir boyut
            };
            inputs.Add(paramLabel);

            double maxLabelWidth = MaxLabelWidth(model);

            var properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                // Responsive Grid yapısını oluştur
                var grid = new Grid
                {
                    Margin = new Thickness(5),
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch // Stretch kullanarak genişlik ayarlaması
                };

                // İki sütun tanımla (Label ve Input için), Responsive genişlik için ayarlar
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(maxLabelWidth, GridUnitType.Pixel) }); // Label genişliği en uzun olan
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Input genişliği dinamik

                // Property için Label oluştur (TextBlock)
                var label = new TextBlock
                {
                    Text = prop.Name + ":",
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 5, 10, 5)
                };

                // Label'ı Grid'in ilk sütununa ekle
                Grid.SetColumn(label, 0);
                grid.Children.Add(label);

                // Property türüne göre input kontrolünü oluştur
                UIElement inputControl = CreateInputControlForProperty(prop, model);

                if (inputControl != null)
                {
                    // Responsive için genişliği otomatik yap
                    if (inputControl is System.Windows.Controls.Control control)
                    {
                        control.MinWidth = 100; // Minimum genişlik ayarı
                        control.Width = Double.NaN; // Otomatik genişlik için NaN atanır
                        control.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    }

                    // Input kontrolünü Grid'in ikinci sütununa ekle
                    Grid.SetColumn(inputControl, 1);
                    grid.Children.Add(inputControl);

                    inputs.Add(grid);
                }
            }

            return inputs;
        }

        // Her bir property'ye uygun kontrol oluşturur
        private static UIElement CreateInputControlForProperty(PropertyInfo property, object model)
        {
            var propertyType = property.PropertyType;
            var currentValue = property.GetValue(model)?.ToString() ?? "";

            // String ve numeric tipler için TextBox
            if (propertyType == typeof(string) || propertyType == typeof(int) || propertyType == typeof(double))
            {
                var textBox = new System.Windows.Controls.TextBox
                {
                    Text = currentValue,
                    Width = 200,
                    Margin = new Thickness(0, 5, 0, 5),
                    Tag = property.Name,
                    ToolTip = $"Enter value for {property.Name}" // Tooltip ekleme
                };

                // Sadece sayı için kısıtlama (int)
                if (propertyType == typeof(int))
                {
                    textBox.PreviewTextInput += (sender, e) =>
                    {
                        e.Handled = !int.TryParse(e.Text, out _);
                    };
                }

                return textBox;
            }
            // Boolean için CheckBox
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                return new System.Windows.Controls.CheckBox
                {
                    IsChecked = bool.TryParse(currentValue, out var result) && result,
                    Margin = new Thickness(0, 5, 0, 5),
                    Tag = property.Name, // Property adını Tag olarak tutuyoruz
                    ToolTip = $"Enter value for {property.Name}" // Tooltip ekleme
                };
            }
            // Enum tipi için ComboBox
            else if (propertyType.IsEnum)
            {
                var comboBox = new System.Windows.Controls.ComboBox
                {
                    ItemsSource = Enum.GetValues(propertyType),
                    SelectedItem = Enum.Parse(propertyType, currentValue),
                    Width = 200,
                    Margin = new Thickness(0, 5, 0, 5),
                    Tag = property.Name, // Property adını Tag olarak tutuyoruz
                    ToolTip = $"Enter value for {property.Name}" // Tooltip ekleme
                };
                return comboBox;
            }
            // DateTime için DatePicker
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                return new DatePicker
                {
                    SelectedDate = DateTime.TryParse(currentValue, out var date) ? date : (DateTime?)null,
                    Margin = new Thickness(0, 5, 0, 5),
                    Tag = property.Name,
                    ToolTip = $"Enter value for {property.Name}" // Tooltip ekleme
                };
            }
            // List<string> tipi için ListBox
            else if (propertyType == typeof(List<string>))
            {
                var listBox = new System.Windows.Controls.ListBox
                {
                    SelectionMode = System.Windows.Controls.SelectionMode.Multiple,
                    ItemsSource = (List<string>)property.GetValue(model),
                    Width = 200,
                    Height = 100,
                    Margin = new Thickness(0, 5, 0, 5),
                    Tag = property.Name,
                    ToolTip = $"Enter value for {property.Name}" // Tooltip ekleme
                };

                return listBox;
            }

            return null;
        }

        // Dinamik olarak oluşturulan input'lardan alınan değerleri modeldeki property'lere set eder
        public static void UpdateModelFromInputs(object model, List<UIElement> inputs)
        {
            if (model == null && inputs == null)
                return;

            if (inputs.Count <= 0)
                return;

            var properties = model.GetType().GetProperties();

            foreach (var input in inputs)
            {
                // Grid içerisindeki kontrolleri al
                if (input is Grid grid)
                {
                    foreach (UIElement element in grid.Children)
                    {
                        // Her bir input'un Tag'inde property ismi saklı
                        var propertyName = (string)element.GetValue(FrameworkElement.TagProperty);
                        var property = properties.FirstOrDefault(p => p.Name == propertyName);
                        if (property == null) continue;

                        if (element is System.Windows.Controls.TextBox textBox)
                        {
                            var convertedValue = Convert.ChangeType(textBox.Text, property.PropertyType);
                            property.SetValue(model, convertedValue);
                        }
                        else if (element is System.Windows.Controls.CheckBox checkBox)
                        {
                            property.SetValue(model, checkBox.IsChecked);
                        }
                        else if (element is System.Windows.Controls.ComboBox comboBox)
                        {
                            property.SetValue(model, comboBox.SelectedItem);
                        }
                        else if (input is DatePicker datePicker)
                        {
                            property.SetValue(model, datePicker.SelectedDate);
                        }
                        else if (input is System.Windows.Controls.ListBox listBox)
                        {
                            var selectedItems = listBox.SelectedItems.Cast<string>().ToList();
                            property.SetValue(model, selectedItems);
                        }
                    }
                }
            }
        }
    }
}
