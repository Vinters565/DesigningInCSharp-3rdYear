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
