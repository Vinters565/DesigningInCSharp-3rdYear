using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Controls;
using UI.Messages;

namespace UI.UserControls
{
    public partial class CalendarView : UserControl, IViewCalendar
    {
        public string UserName { get; set; } = string.Empty;
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        public IViewCalendar CurrentView
        {
            get => (IViewCalendar)CalendarContentArea.Content;
            private set => CalendarContentArea.Content = value;
        }

        private readonly MonthView monthView;
        private readonly WeekView weekView;

        public CalendarView(bool isPublic)
        {
            InitializeComponent();
            monthView = new(DateTextBlock, isPublic);
            weekView = new(DateTextBlock, isPublic);
            CurrentView = monthView;

            WeakReferenceMessenger.Default.Register<UpdateViewMessage>(this, (r, m) => UpdateView());
            UpdateView();
        }

        private void MonthButton_Click(object sender, RoutedEventArgs e) 
        {
            CurrentView = monthView;
            UpdateView();
        }

        private void WeekButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = weekView;
            UpdateView();
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
