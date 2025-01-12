using System.Windows;
using System.Windows.Controls;
using UI.Windows;

namespace UI.ElementPage
{
    public partial class EditEventPage : Page
    {
        public DateTime? StartDate
        {
            get => StartDatePicker.DateTime;
            set => StartDatePicker.DateTime = value;
        }

        public EditEventPage()
        {
            InitializeComponent();
        }

        private void CreateEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EventComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CanBeCompletedPanel.Visibility = Visibility.Collapsed;
            DependsOnLocationPanel.Visibility = Visibility.Collapsed;
            RecurrencePanel.Visibility = Visibility.Collapsed;

            var selectedItem = eventComboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                var selectedTag = selectedItem.Tag.ToString();

                switch (selectedTag)
                {
                    case "CanBeCompletedEventAttribute":
                        CanBeCompletedPanel.Visibility = Visibility.Visible;
                        break;
                    case "DependsOnLocationEventAttribute":
                        DependsOnLocationPanel.Visibility = Visibility.Visible;
                        break;
                    case "RecurrenceEventAttribute":
                        RecurrencePanel.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void ResetAttribute_Click(object sender, RoutedEventArgs e)
        {
            eventComboBox.SelectedIndex = -1;
        }
    }
}
