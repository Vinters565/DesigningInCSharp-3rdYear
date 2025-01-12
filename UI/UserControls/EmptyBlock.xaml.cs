using System.Windows;
using System.Windows.Controls;
using UI.Windows;

namespace UI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для EmptyBlock.xaml
    /// </summary>
    public partial class EmptyBlock : UserControl
    {
        public bool IsPublic { get; set; } = false;

        public EmptyBlock()
        {
            InitializeComponent();
        }

        private void CreateNewCalendarEvent_Click(object sender, RoutedEventArgs e)
        {
            var eventWindow = new NewEventWindow() { StartDate = DateTime.Now, Public = IsPublic};
            eventWindow.Show();
        }
    }
}
