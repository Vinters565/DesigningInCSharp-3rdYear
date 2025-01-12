using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.UserControls
{
    public partial class WeekView : UserControl, IViewCalendar
    {
        private readonly ApiClient client;
        private readonly TextBlock dateTextBlock;

        public DateTime CurrentDate { get; set; }


        public WeekView(TextBlock dateTextBlock)
        {
            InitializeComponent();
            client = new ApiClient();
            this.dateTextBlock = dateTextBlock;
            CurrentDate = DateTime.Now;
            FillTimeColumn();
            FillCalendarArea();
            UpdateWeekView();
        }

        private void FillTimeColumn()
        {
            for (int i = 0; i < 48; i++)
            {
                var border = CreateTimeBorder();
                var record = (i/2).ToString();
                record += i % 2 == 0 ? ":00" : ":30";
                var block = CreateTimeTextBlock(record, Brushes.DarkGray);
                border.Child = block;

                Grid.SetRow(border, i+1);
                Grid.SetColumn(border, 0);
                GridArea.Children.Add(border);
            }
        }

        private Border CreateTimeBorder()
        {
            return new Border
            {
                Style = (Style)Application.Current.FindResource("ViewBorderStyle")
            };
        }

        private TextBlock CreateTimeTextBlock(string text, Brush foreground)
        {
            return new TextBlock
            {
                Text = text,

                Style = (Style)Application.Current.FindResource("ViewTextBlockStyle")
            };
        }

        private void FillCalendarArea()
        {
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 48; j++)
                {
                    var block = new EmptyBlock { IsPublic = false };
                    Grid.SetColumn(block, 1 + i);
                    Grid.SetRow(block, 1 + j);
                    GridArea.Children.Add(block);
                }
            }
        }

        private void AddEvent(string title, int startRow, int duration)
        {
            var eventBlock = new EventBlock
            {
                EventTitle = title,
                StartRow = startRow,
                Duration = duration
            };

            Grid.SetRow(eventBlock, startRow);
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

