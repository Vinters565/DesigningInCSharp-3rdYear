using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Controls;
using UI.Messages;

namespace UI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ButtonShowPublicCalendar.xaml
    /// </summary>
    public partial class ButtonShowPublicCalendar : UserControl
    {
        public UserControl Calendar { get; set; }

        public ButtonShowPublicCalendar()
        {
            InitializeComponent();
        }

        private void Event_Click(object sender, RoutedEventArgs e)
        {
            //WeakReferenceMessenger.Default.Send(new ViewCalendarMessage(Calendar));
        }
    }
}
