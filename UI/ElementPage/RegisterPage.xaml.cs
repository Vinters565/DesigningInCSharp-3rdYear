using System.Windows;
using System.Windows.Controls;
using UI.Windows;
using UI.Dto;
using CommunityToolkit.Mvvm.Messaging;
using UI.Messages;

namespace UI.ElementPage
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegistrPage : Page
    {
        public RegistrPage()
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
                MessageBox.Show($"Успешная регистрация!");
                WeakReferenceMessenger.Default.Send(new OpenLoginPageMessage());
            }
            catch (Exception ex)

            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
