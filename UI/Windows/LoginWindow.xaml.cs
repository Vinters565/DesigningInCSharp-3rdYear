using System;
using System.Configuration;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;
using UI.ElementPage;
using UI.Messages;

namespace UI.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            OpenLoginPage();
            WeakReferenceMessenger.Default.Register<OpenLoginPageMessage>(this, (r, m) => OpenLoginPage());
            WeakReferenceMessenger.Default.Register<OpenMainWindowMessage>(this, (r, m) => OpenMainWindow());
            WeakReferenceMessenger.Default.Register<OpenRegisterPageMessage>(this, (r, m) => OpenRegisterPage());
        }

        private void OpenRegisterPage()
        {
            LoginFrame.NavigationService.Navigate(new RegistrPage());
        }

        private void OpenMainWindow()
        {
            new MainWindow().Show();
            Close();
        }

        private void OpenLoginPage()
        {
            LoginFrame.NavigationService.Navigate(new LoginPage());
        }
    }
}

