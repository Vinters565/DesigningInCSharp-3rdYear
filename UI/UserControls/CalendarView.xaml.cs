using System.Windows;
using System.Windows.Controls;
using UI.Windows;

namespace UI.UserControls
{
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();
            ShowMonthView();
        }

        private void MonthButton_Click(object sender, RoutedEventArgs e) 
        {
            ShowMonthView();
            SetStandartSizeAllColumn();
            SetSizeColumn(ColumnMonth, 2);
        }

        private void WeekButton_Click(object sender, RoutedEventArgs e)
        {
            ShowWeekView();
            SetStandartSizeAllColumn();
            SetSizeColumn(ColumnWeek, 2);
        }
        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDayView();
            SetStandartSizeAllColumn();
            SetSizeColumn(ColumnDay, 2);
        }

        private void ListButton_Click(object sender, RoutedEventArgs e) 
        {
            ShowListView();
            SetStandartSizeAllColumn();
            SetSizeColumn(ColumnList, 2);
        }

        private void CreateNewEvent_Click(object sender, RoutedEventArgs e) 
        {
            var eventWindow = new NewEventWindow();
            eventWindow.Show();
        }

        private void ShowMonthView() 
        {
            CalendarContentArea.Content = new MonthView();
        }

        private void ShowWeekView() 
        {
            CalendarContentArea.Content = new WeekView();
        }

        private void ShowDayView() 
        {
            CalendarContentArea.Content = new DayView();
        }

        private void ShowListView() 
        {
            CalendarContentArea.Content = new ListView();
        }

        private void SetStandartSizeAllColumn()
        {
            SetSizeColumn(ColumnList, 1);
            SetSizeColumn(ColumnDay, 1);
            SetSizeColumn(ColumnWeek, 1);
            SetSizeColumn(ColumnMonth, 1);
        }

        private void SetSizeColumn(ColumnDefinition column, double value)
        {
            column.Width = new GridLength(value, GridUnitType.Star);
        }
    }
}
