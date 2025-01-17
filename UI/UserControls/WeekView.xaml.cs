using System.DirectoryServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.UserControls
{
    public partial class WeekView : UserControl, IViewCalendar
    {
        private readonly TextBlock dateTextBlock;
        private readonly ApiClient apiClient;

        public DateTime CurrentDate { get; set; }

        public WeekView(TextBlock dateTextBlock)
        {
            InitializeComponent();
            this.dateTextBlock = dateTextBlock;
            apiClient = new ApiClient();
            CurrentDate = DateTime.Now;
            FillTimeColumn();
            FillGridCells();
            FillCellsGridArea();
            FillCalendarEvents();
            UpdateWeekView();
        }

        private async void FillCalendarEvents()
        {
            var events = await apiClient.GetPrivateEventsAsync(CurrentDate.AddDays(-(int)CurrentDate.DayOfWeek + 1), 3);
            foreach (var e in events)
            {
                var date = (e.End - e.Start).Hours;
                AddEvent(e.Name, (int)e.Start.DayOfWeek, e.Start.Hour, 1 * 6);
            }
        }

        private void FillTimeColumn()
        {
            for (int i = 0; i < 48; i++)
            {
                var border = new Border {
                    Style = (Style)Application.Current.FindResource("ViewBorderStyle") };

                var record = (i/2).ToString();
                record += i % 2 == 0 ? ":00" : ":30";
                var block = new TextBlock { 
                    Text = record, Style = (Style)Application.Current.FindResource("ViewTextBlockStyle") };

                border.Child = block;

                Grid.SetRow(border, i+1);
                Grid.SetColumn(border, 0);
                GridArea.Children.Add(border);
            }
        }

        private void FillGridCells()
        {
            for (int i = 0; i < 7; i++)
            {
                var columnDefinition = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star)};
                EventsGrid.ColumnDefinitions.Add(columnDefinition);
            }
            for (int i = 0; i < 48 * 3; i++)
            {
                var rowDefinition = new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) };
                EventsGrid.RowDefinitions.Add(rowDefinition);
            }
        }

        private void FillCellsGridArea()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 48 * 3; j += 3)
                {
                    var block = new EmptyBlock { IsPublic = false };
                    Grid.SetColumn(block, i);
                    Grid.SetRowSpan(block, 3);
                    Grid.SetRow(block, j);
                    EventsGrid.Children.Add(block);
                }
            }
        }

        private void AddEvent(string title, int startColumn, int startRow, int duration)
        {
            var eventBlock = new EventBlock(title, startColumn, startRow, duration);
            Grid.SetRow(eventBlock, startRow * 6);
            Grid.SetColumn(eventBlock, startColumn);
            Grid.SetRowSpan(eventBlock, duration);

            EventsGrid.Children.Add(eventBlock);
        }

        private void UpdateWeekView()
        {
            DateTime startOfWeek = CurrentDate.AddDays(-(int)CurrentDate.DayOfWeek + 1); 
            DateMon.Text = $"Mon {startOfWeek:dd/MM}";
            DateTue.Text = $"Tue {startOfWeek.AddDays(1):dd/MM}";
            DateWed.Text = $"Wed {startOfWeek.AddDays(2):dd/MM}";
            DateThu.Text = $"Thu {startOfWeek.AddDays(3):dd/MM}";
            DateFri.Text = $"Fri {startOfWeek.AddDays(4):dd/MM}";
            DateSat.Text = $"Sat {startOfWeek.AddDays(5):dd/MM}";
            DateSun.Text = $"Sun {startOfWeek.AddDays(6):dd/MM}";

            dateTextBlock.Text = $"{startOfWeek:dd/MM/yyyy} - {startOfWeek.AddDays(6):dd/MM/yyyy}";
        }

        private void PreviousWeek_Click(object sender, RoutedEventArgs e)
        {
            PrevView();
        }

        private void NextWeek_Click(object sender, RoutedEventArgs e)
        {
            NextView();
        }

        public void NextView()
        {
            CurrentDate = CurrentDate.AddDays(7);
            UpdateWeekView();
        }

        public void PrevView()
        {
            CurrentDate = CurrentDate.AddDays(-7);
            UpdateWeekView();
        }

        public void UpdateView()
        {
            UpdateWeekView();
        }
    }
}

