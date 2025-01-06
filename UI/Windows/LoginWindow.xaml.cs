using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}

