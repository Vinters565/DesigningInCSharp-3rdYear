using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            var authService = new ApiClient();
            var registrRequest = new RegisterUserRequest
            {
                Username = UsernameTextBox.Text,
                DisplayedName = DisplayedNameBox.Text,
                Password = PasswordBox.Password
            };

            try
            {
                string token = await authService.RegisterAsync(registrRequest);
                MessageBox.Show($"Успешный вход! Токен: {token}");
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }
    }
}
