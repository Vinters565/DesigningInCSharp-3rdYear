using System.Windows;
using System.Windows.Controls;

namespace UI.UserControls
{
    public partial class CalendarView : UserControl, IViewCalendar
    {
        public bool IsPublic { get; set; } = false;
        public string UserName { get; set; } = string.Empty;
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        public IViewCalendar CurrentView
        {
            get => (IViewCalendar)CalendarContentArea.Content;
            private set => CalendarContentArea.Content = value;
        }

        private readonly MonthView monthView;
        private readonly WeekView weekView;

        public CalendarView()
        {
            InitializeComponent();
            monthView = new(DateTextBlock);
            weekView = new(DateTextBlock);
            CurrentView = monthView;
            UpdateView();
        }

        private void MonthButton_Click(object sender, RoutedEventArgs e) 
        {
            CurrentView = monthView;
        }

        private void WeekButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = weekView;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            NextView();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            PrevView();
        }

        public void NextView()
        {
            CurrentView.NextView();
        }

        public void PrevView()
        {
            CurrentView.PrevView();
        }

        public void UpdateView()
        {
            CurrentView.UpdateView();
        }
    }
}
