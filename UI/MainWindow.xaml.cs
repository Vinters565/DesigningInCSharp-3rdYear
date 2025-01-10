using CommunityToolkit.Mvvm.Messaging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        public MainWindow()
        {
            InitializeComponent();
            if (TokenFileStorage.GetToken() == null)
            {
                OpenAuthWindow();
            }
            client = new ApiClient();

            OpenPage(new CalendarPage());
            WeakReferenceMessenger.Default.Register<OpenPersonalPageMessage>(this, (r, m) => OpenPage(new PersonalAccountPage()));
            WeakReferenceMessenger.Default.Register<ExitAccountMessage>(this, (r, m) => OpenAuthWindow());
        }

        private void OpenAuthWindow()
        {
            TokenFileStorage.DeleteToken();
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void OpenPage(Page page)
        {
            MainFrame.NavigationService.Navigate(page);
        }
    }
}