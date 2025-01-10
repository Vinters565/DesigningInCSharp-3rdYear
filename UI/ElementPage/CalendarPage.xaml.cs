using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Messages;

namespace UI.ElementPage
{
    /// <summary>
    /// Логика взаимодействия для CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            InitializeComponent();
            OpenCalendarPage(new PrivateCalendarPage());
        }

        private void OpenCalendarPage(Page page)
        {
            CalendarFrame.NavigationService.Navigate(page);
        }

        private void OpenPersonalAccountPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new OpenPersonalPageMessage());
        }
    }
}
