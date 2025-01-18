using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Dto;
using UI.Messages;
using UI.UserControls;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            var token = new JwtSecurityTokenHandler().ReadToken(TokenFileStorage.GetToken()) as JwtSecurityToken;
            foreach (var claim in token.Claims)
            {
                if (claim.Type == "userName")
                {
                    UserName.Text = claim.Value;
                }
            }
            OpenCalendarPage(new CalendarView(false));
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
            OpenCalendarPage(new CalendarView(false));
        }

        private void OpenPublicCalendar_Click(object sender, RoutedEventArgs e)
        {
            OpenCalendarPage(new CalendarView(true));
        }

        private void OpenListPublicCalendars_Click(object sender, RoutedEventArgs e)
        {
            OpenCalendarPage(new ListPublicCalendars(2, 2));
        }
    }
}
