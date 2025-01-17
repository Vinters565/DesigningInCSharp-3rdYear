using System.Windows;
using System.Windows.Controls;
using UI.Windows;

namespace UI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для EventBlock.xaml
    /// </summary>
    public partial class EventBlock : UserControl
    {
        public EventBlock(string title, int startColumn, int startRow, int duration)
        {
            InitializeComponent();
            StartColumn = startColumn;
            StartRow = startRow;
            Duration = duration;
            Title.Text = title;
        }

        public int StartRow;

        public int Duration;
        public int StartColumn;

        private void OpenInfo_Click(object sender, RoutedEventArgs e)
        {
            var eventWindow = new NewEventWindow(false, DateTime.Now, false);
            eventWindow.Show();
        }
    }
}
