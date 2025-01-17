﻿using CommunityToolkit.Mvvm.Messaging;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using UI.Dto;
using UI.ElementPage;
using UI.Messages;
using UI.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApiClient client;
        private readonly SubscribePage subscribePage;
        private readonly CalendarPage calendarPage;

        public MainWindow()
        {
            InitializeComponent();
            if (TokenFileStorage.GetToken() == null)
            {
                OpenAuthWindow();
            }
            client = new ApiClient();
            subscribePage = new SubscribePage();
            calendarPage = new CalendarPage();
            OpenPage(MainFrame, calendarPage);
            OpenPage(SecondFrame, subscribePage);
            WeakReferenceMessenger.Default.Register<OpenPersonalPageMessage>(this, (r, m) => OpenPage(MainFrame, new PersonalAccountPage()));
            WeakReferenceMessenger.Default.Register<BackMessage>(this, (r, m) => OpenPage(MainFrame, calendarPage));
            WeakReferenceMessenger.Default.Register<ExitAccountMessage>(this, (r, m) => OpenAuthWindow());
            WeakReferenceMessenger.Default.Register<ViewCalendarMessage>(this, (recipient, message) => ShowPublicCalendar(message.Value));
            WeakReferenceMessenger.Default.Register<CloseSubscribePageMessage>(this, (r, m) => SecondFrame.Visibility = Visibility.Hidden);
        }

        private void OpenAuthWindow()
        {
            TokenFileStorage.DeleteToken();
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void OpenPage(Frame frame, Page page)
        {
            frame.NavigationService.Navigate(page);
        }

        private void ShowPublicCalendar(UserControl calendar)
        {
            SecondFrame.Visibility = Visibility.Visible;
            subscribePage.Show(calendar);
        }
    }
}