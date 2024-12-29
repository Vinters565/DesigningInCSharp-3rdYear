using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class DayView : UserControl
    {
        private DateTime currentDate;

        public DayView()
        {
            InitializeComponent();
            currentDate = DateTime.Today; 
            UpdateDateText(); 
        }

        private void PreviousDay_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(-1); 
            UpdateDateText(); 
        }

        private void NextDay_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(1); 
            UpdateDateText(); 
        }

        private void UpdateDateText()
        {
            DayDateText.Text = currentDate.ToString("ddd dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
    }
}
