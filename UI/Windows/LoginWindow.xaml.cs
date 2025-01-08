using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using UI.Dto;

namespace UI.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OpenRegisterWindow_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Close();
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
                MessageBox.Show($"Успешный вход! Токен: {token}");
                TokenFileStorage.SaveToken(token);
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}

