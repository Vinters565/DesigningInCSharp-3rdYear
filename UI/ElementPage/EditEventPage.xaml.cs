using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using UI.Repository;
using UI.Windows;
using UI.Dto;
using System;

namespace UI.ElementPage
{
    public partial class EditEventPage : Page
    {
        private List<string> attributesUsed = new();
        private Dictionary<string, Dictionary<string, Func<object>>> attributesValues = new();

        public DateTime? StartDate
        {
            get => StartDatePicker.DateTime;
            set => StartDatePicker.DateTime = value;
        }

        public DateTime? EndDate
        {
            get => EndDatePicker.DateTime;
            set => EndDatePicker.DateTime = value;
        }

        public EditEventPage(DateTime startdate)
        {
            InitializeComponent();
            EventNameTextBox.Text = "Новое событие";
            StartDate = startdate;
            var endTime = startdate.AddHours(1);
            EndDate = endTime;
            LoadAttribute();
        }

        private async void LoadAttribute()
        {
            var attributes = await App.ServiceProvider.GetRequiredService<IAttributeRepository>().GetAttributesAsync();
            eventComboBox.ItemsSource = attributes.Where(attribute => !attributesUsed.Contains(attribute.Name))
                .Select(attribute => 
                    new ComboBoxItem 
                    { 
                        Content = attribute.Description,
                        Tag = attribute.Name 
                    })
                .ToArray();
        }

        private async void CreateEvent_Click(object sender, RoutedEventArgs e)
        {
            var attributes = new Dictionary<string, Dictionary<string, object>>();
            foreach (var attributeValue in attributesValues)
            {
                var dict = new Dictionary<string, object>();
                foreach (var functionValueAttribute in attributeValue.Value)
                {
                    dict.Add(functionValueAttribute.Key, functionValueAttribute.Value());
                }
                attributes.Add(attributeValue.Key, dict);
            }

            var calendarEvent = new CreateCalendarEventRequest()
            {
                Name = EventNameTextBox.Text,
                Start = StartDate.Value,
                End = EndDate.Value,
                Attributes = attributes
            };
            await App.ServiceProvider.GetRequiredService<ApiClient>().CreateEventsAsync(calendarEvent);
        }

        private async void EventComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = eventComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                var attributes = await App.ServiceProvider.GetRequiredService<IAttributeRepository>().GetAttributesAsync();
                var attribute = attributes.Where(attribute => attribute.Name == (string)selectedItem.Tag).First();
                if (attribute != null)
                {
                    var field = AttributeFormGenerator.GenerateFullAtributeField(attribute);
                    attributesValues.Add(attribute.Name, field.Item2);
                    var fieldWithDeleteButton = AddButtonDelete(field.Item1, attribute.Name);
                    ContainerAttributeField.Children.Add(fieldWithDeleteButton);
                    attributesUsed.Add(attribute.Name);
                    LoadAttribute();
                }
                eventComboBox.SelectedIndex = -1;
            }
        }

        private Grid AddButtonDelete(StackPanel stackPanel, string attributeName)
        {
            var grid = new Grid() { Name = attributeName, Margin = new Thickness(5, 5, 5, 10)};
            grid.ColumnDefinitions.Add(new ColumnDefinition() );
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(25, GridUnitType.Pixel) });
            var button = new Button() { Content = "✖", Width = 20, Height = 20, Margin = new Thickness(5, 0, 0, 0) };
            button.Click += ((x, y) => DeleteAttribute_Click(attributeName));

            Grid.SetColumn(stackPanel, 0);
            Grid.SetColumn(button, 1);

            grid.Children.Add(stackPanel);
            grid.Children.Add(button);
            return grid;
        }

        private void ResetAttribute_Click(object sender, RoutedEventArgs e)
        {
            eventComboBox.SelectedIndex = -1;
        }

        private void DeleteAttribute_Click(string nameAttribute)
        {
            attributesUsed.Remove(nameAttribute);
            attributesValues.Remove(nameAttribute);
            foreach (UIElement element in ContainerAttributeField.Children)
            {
                if (((Grid)element).Name == nameAttribute)
                {
                    ContainerAttributeField.Children.Remove(element);
                    break;
                }
            }
            LoadAttribute();
        }
    }
}
