using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void WeekButton_Click(object sender, RoutedEventArgs e)
        {
            ShowWeekView();
        }
        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDayView();
        }

        private void ListButton_Click(object sender, RoutedEventArgs e) 
        {
            ShowListView();
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
    }
}
