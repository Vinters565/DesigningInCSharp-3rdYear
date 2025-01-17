using System.Windows.Controls;
using System.Windows;
using UI.Dto;
using System.Text.Json;
using Syncfusion;
using Syncfusion.Windows.Shared;
using System;

namespace UI;
public static class AttributeFormGenerator
{
    public static StackPanel GenerateForm(List<AttributeMetadata> attributes)
    {
        StackPanel panel = new StackPanel();

        foreach (var attribute in attributes)
        {
            panel.Children.Add(GenerateFullAtributeField(attribute).Item1);
        }

        return panel;
    }

    public static Tuple<StackPanel, Dictionary<string, Func<object>>> GenerateFullAtributeField(AttributeMetadata attribute)
    {
        var areaField = new StackPanel()
        {
            Orientation = Orientation.Vertical
        };
        var fieldWithFunction = new Dictionary<string, Func<object>>();

        var headerField = GenerateHeaderFieldConttroll(attribute);
        areaField.Children.Add(headerField.Item1);
        fieldWithFunction.Add(attribute.Fields.First().FieldName, headerField.Item2);
        foreach (var field in attribute.Fields.Skip(1))
        {
            var fieldControll = GenerateFieldControl(field);
            areaField.Children.Add(fieldControll.Item1);
            fieldWithFunction.Add(field.FieldName, fieldControll.Item2);
        }

        return new (areaField, fieldWithFunction);
    }

    public static Tuple<StackPanel, Func<object>> GenerateHeaderFieldConttroll(AttributeMetadata atribute)
    {
        var header = new TextBlock
        {
            Text = atribute.Description,
            FontSize = 16,
            FontWeight = System.Windows.FontWeights.Bold,
            Margin = new Thickness(0, 10, 0, 5),
            HorizontalAlignment = HorizontalAlignment.Left
        };

        var headerField = new StackPanel 
        { 
            Orientation = Orientation.Horizontal,
            Width = 400,
            Height = 40,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        
        var setting = GenerateBoolInputField(atribute.Fields.First());
        setting.Margin = new Thickness(5, 15, 0, 5);
        setting.HorizontalAlignment = HorizontalAlignment.Right;

        headerField.Children.Add(header);
        headerField.Children.Add(setting);

        return new(headerField, () => setting.IsChecked);
    }

    public static Tuple<StackPanel, Func<object>> GenerateFieldControl(AttributeField field)
    {
        var fieldPanel = new StackPanel { Margin = new Thickness(0, 5, 0, 5) };
        var label = new TextBlock { Text = field.Description, FontWeight = System.Windows.FontWeights.SemiBold };

        UIElement inputElement;
        Func<object> func;
        switch (field.FieldType.ToLower())
        {
            case "string":
                var elementString = GenerateStringInputField(field);
                inputElement = elementString;
                func = new Func<object>(() => elementString.Text);
                break;

            case "int":
            case "double":
                var elementNumeric = GenerateNumericInputField(field);
                inputElement = elementNumeric;
                func = new Func<object>(() => elementNumeric.Text);
                break;

            case "bool":
                var elementBool = GenerateBoolInputField(field);
                inputElement = elementBool;
                func = new Func<object>(() => elementBool!.IsChecked);
                break;

            case "enum":
                var elementCheckBox = GenerateEnumInputField(field);
                inputElement = elementCheckBox;
                func = new Func<object>(() => elementCheckBox.SelectedItem);
                break;

            case "datetime":
                var elementDateTime = GenerateDateTimeInputField(field);
                inputElement = elementDateTime;
                func = new Func<object>(() => elementDateTime.DateTime.Value);
                break;

            default:
                inputElement = new TextBlock { Text = "Неизвестный тип поля" };
                func = new Func<object>(() => "");
                break;
        }

        fieldPanel.Children.Add(label);
        fieldPanel.Children.Add(inputElement);
        return new (fieldPanel, func);
    }

    private static TextBox GenerateStringInputField(AttributeField field)
    {
        return new TextBox
        {
            Text = field.DefaultValue?.ToString() ?? "",
            Width = 200,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(5)
        };
    }

    private static TextBox GenerateNumericInputField(AttributeField field)
    {
        return new TextBox
        {
            Text = field.DefaultValue?.ToString() ?? "0",
            Width = 100,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(5)
        };
    }

    private static CheckBox GenerateBoolInputField(AttributeField field)
    {
        return new CheckBox
        {
            IsChecked = field.DefaultValue as bool? ?? false,
            IsThreeState = false,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(5)
        };
    }

    private static ComboBox GenerateEnumInputField(AttributeField field)
    {
        var selectedItem = ((JsonElement)field.DefaultValue).GetString() 
            ?? (field.PossibleChoices?.Count > 0 ? field.PossibleChoices[0] : null);
        return new ComboBox
        {
            ItemsSource = field.PossibleChoices,
            SelectedItem = selectedItem,
            Width = 150,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(5)
        };
    }

    private static DateTimeEdit GenerateDateTimeInputField(AttributeField field)
    {
        return new DateTimeEdit
        {
            Pattern = DateTimePattern.FullDateTime,
            Margin = new Thickness(5),
            HorizontalAlignment = HorizontalAlignment.Left,
            MaxWidth = 200
        };
    }
}