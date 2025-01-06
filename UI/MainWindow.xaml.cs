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
            client = new ApiClient();
        }

        private void OpenPersonalAccountWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var personalAccountWindow = new PersonalAccountWindow();
            personalAccountWindow.Show();
        }

        private void OpenAuthWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}