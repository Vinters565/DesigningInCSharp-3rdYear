using System.Windows;
using System.Windows.Controls;
using UI.Windows;
using UI.Dto;

namespace UI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для EventBlock.xaml
    /// </summary>
    public partial class EventBlock : UserControl
    {
        public int StartRow { get; private set; }

        public int Duration { get; private set; }
        public int StartColumn { get; private set; }
        public CalendarEventDto CalendarEvent { get; private set; }

        public EventBlock(int startColumn, int startRow, int duration, CalendarEventDto calendarEvent)
        {
            InitializeComponent();
            StartColumn = startColumn;
            StartRow = startRow;
            Duration = duration;
            Title.Text = "Новое событие";
            CalendarEvent = calendarEvent;
        }

        private void OpenInfo_Click(object sender, RoutedEventArgs e)
        {
            var eventWindow = new NewEventWindow(CalendarEvent);
            eventWindow.Show();
        }
    }
}
