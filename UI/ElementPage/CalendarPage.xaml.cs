using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Messages;
using UI.UserControls;

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
            OpenCalendarPage(new CalendarView() { IsPublic = false } );
        }

        private void OpenCalendarPage(UserControl page)
        {
            CalendarFrame.NavigationService.Navigate(page);
        }

        private void OpenPersonalAccountPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new OpenPersonalPageMessage());
        }

        private void OpenPrivateCalendar_Click(object sender, RoutedEventArgs e)
        {
            OpenCalendarPage(new CalendarView() { IsPublic = false });
        }

        private void OpenPublicCalendar_Click(object sender, RoutedEventArgs e)
        {
            OpenCalendarPage(new CalendarView() { IsPublic = true });
        }

        private void OpenListPublicCalendars_Click(object sender, RoutedEventArgs e)
        {
            OpenCalendarPage(new ListPublicCalendars(2, 2));
        }
    }
}
