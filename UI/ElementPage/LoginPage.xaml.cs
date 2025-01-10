using System;
using System.Windows;
using System.Windows.Controls;
using UI.Dto;
using CommunityToolkit.Mvvm.Messaging;
using UI.Messages;

namespace UI.ElementPage
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OpenRegisterWindow_Click(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new OpenRegisterPageMessage());
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var authService = new ApiClient();
            var loginRequest = new LoginUserRequest
            {
                Username = UsernameTextBox.Text,
                Password = PasswordBox.Password
            };

            try
            {
                string token = await authService.LoginAsync(loginRequest);
                MessageBox.Show($"Успешный вход!");
                TokenFileStorage.SaveToken(token);
                WeakReferenceMessenger.Default.Send(new OpenMainWindowMessage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
