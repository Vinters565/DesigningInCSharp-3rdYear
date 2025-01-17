using System.Windows;
using System.Windows.Controls;
using UI.Windows;

namespace UI.ElementPage
{
    public partial class EventInfoPage : Page
    {
        public EventInfoPage()
        {
            InitializeComponent();
        }

        private void EditEventButton_Click(object sender, RoutedEventArgs e)
        {
            var editEventPage = new EditEventPage(DateTime.Now);
            NavigationService.Navigate(editEventPage);
        }

        private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
