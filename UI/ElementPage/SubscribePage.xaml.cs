using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Controls;
using UI.Messages;

namespace UI.ElementPage
{
    /// <summary>
    /// Логика взаимодействия для SubscribePage.xaml
    /// </summary>
    public partial class SubscribePage : Page
    {
        public SubscribePage()
        {
            InitializeComponent();
        }

        public void Show(UserControl calendar)
        {
            CalendarFrame.NavigationService.Navigate(calendar);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new CloseSubscribePageMessage());
        }

        private void Canel_Click(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new CloseSubscribePageMessage());
        }
    }
}
