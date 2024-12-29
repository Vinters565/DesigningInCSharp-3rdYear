using System.Windows;
using System.Windows.Controls;

namespace UI.UserControls
{
    public partial class WeekView : UserControl
    {
        private DateTime currentDate;

        public WeekView()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
            UpdateWeekView();
        }

        private void UpdateWeekView()
        {
            DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + 1); 
            DateMon.Text = $"Mon {startOfWeek:dd/MM}";
            DateTue.Text = $"Tue {startOfWeek.AddDays(1):dd/MM}";
            DateWed.Text = $"Wed {startOfWeek.AddDays(2):dd/MM}";
            DateThu.Text = $"Thu {startOfWeek.AddDays(3):dd/MM}";
            DateFri.Text = $"Fri {startOfWeek.AddDays(4):dd/MM}";
            DateSat.Text = $"Sat {startOfWeek.AddDays(5):dd/MM}";
            DateSun.Text = $"Sun {startOfWeek.AddDays(6):dd/MM}";

            WeekDateText.Text = $"{startOfWeek:dd/MM/yyyy} - {startOfWeek.AddDays(6):dd/MM/yyyy}";
        }

        private void PreviousWeek_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(-7);
            UpdateWeekView();
        }

        private void NextWeek_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(7);
            UpdateWeekView();
        }
    }
}

