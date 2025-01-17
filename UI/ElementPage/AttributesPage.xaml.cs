using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Dto;
using UI.UserControls;

namespace UI.ElementPage;

public partial class AttributesPage : Page
{
    private readonly List<AttributeMetadata> _attributes;

    public AttributesPage(List<AttributeMetadata> attributes)
    {
        InitializeComponent();
        _attributes = attributes;
        LoadAttributes();
    }

    private void LoadAttributes()
    {
        AttributesContainer.Children.Clear();
        foreach (var attribute in _attributes)
        {
            var control = new AttributeMetadataControl(attribute);
            AttributesContainer.Children.Add(control);
        }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var values = new Dictionary<string, object>();

        foreach (AttributeMetadataControl control in AttributesContainer.Children)
        {
            var attribute = control.DataContext as AttributeMetadata;
            if (attribute == null) continue;

            foreach (var field in attribute.Fields)
            {
                var uiElement = FindUIElementForField(control, field);
                if (uiElement == null) continue;

                values[field.FieldName] = GetValueFromUIElement(uiElement, field);
            }
        }

        MessageBox.Show($"Собрано {values.Count} значений.");
    }

    private UIElement FindUIElementForField(AttributeMetadataControl control, AttributeField field)
    {
        return control.Content switch
        {
            StackPanel stackPanel => stackPanel.Children.OfType<StackPanel>()
                .Select(sp => sp.Children[0])
                .FirstOrDefault(child => child.GetValue(FrameworkElement.TagProperty) == field),
            _ => null
        };
    }

    private object GetValueFromUIElement(UIElement element, AttributeField field)
    {
        return element switch
        {
            TextBox textBox => field.FieldType.ToLower() switch
            {
                "int" => int.TryParse(textBox.Text, out var i) ? i : 0,
                "double" => double.TryParse(textBox.Text, out var d) ? d : 0.0,
                _ => textBox.Text
            },
            CheckBox checkBox => checkBox.IsChecked ?? false,
            ComboBox comboBox => comboBox.SelectedItem?.ToString() ?? "",
            Slider slider => slider.Value,
            _ => null
        };
    }
}
