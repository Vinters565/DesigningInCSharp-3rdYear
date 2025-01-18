using Microsoft.Extensions.DependencyInjection;
using System.DirectoryServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UI.Dto;
using UI.Repository;

namespace UI.UserControls
{
    public partial class WeekView : UserControl, IViewCalendar
    {
        private readonly TextBlock dateTextBlock;
        private readonly List<EmptyBlock> emptyBlock = new();
        private bool isPublic;
        private string? userName;

        public DateTime CurrentDate { get; set; }

        public WeekView(TextBlock dateTextBlock, bool isPublic)
        {
            InitializeComponent();
            this.dateTextBlock = dateTextBlock;
            CurrentDate = DateTime.Now;
            this.isPublic = isPublic;
            FillGridCells();
            FillCalendar();
        }

        public WeekView(TextBlock dateTextBlock, string userName)
        {
            InitializeComponent();
            this.dateTextBlock = dateTextBlock;
            CurrentDate = DateTime.Now;
            isPublic = true;
            this.userName = userName;
            FillGridCells();
            FillCalendar();
        }

        private void FillCalendar()
        {
            Clear();
            FillTimeColumn();
            FillCellsGridArea();
            FillCalendarEvents();
            UpdateWeekView();
        }

        private void Clear()
        {
            foreach (var block in emptyBlock)
            {
                GridArea.Children.Remove(block);
            }
            emptyBlock.Clear();
            EventsGrid.Children.Clear();
        }

        private async void FillCalendarEvents()
        {

            var events = await LoadEvents();
            foreach (var e in events)
            {
                if (e.End.Day <= CurrentDate.AddDays(-(int)CurrentDate.DayOfWeek + 8).Day)
                {
                    AddEvent(e);
                }
            }
        }

        private async Task<List<CalendarEventDto>> LoadEvents()
        {
            var startDay = CurrentDate.AddDays(-(int)CurrentDate.DayOfWeek + 1);
            var startDate = new DateTime(startDay.Year, startDay.Month, startDay.Day);
            var events = userName != null && isPublic 
                ? await App.ServiceProvider.GetRequiredService<ApiClient>().GetPublicEventsAsync(userName, startDate, 2)
                : await App.ServiceProvider.GetRequiredService<ApiClient>().GetPrivateEventsAsync(startDate, 2);
            return isPublic ? events.Where(e => e.Attributes.ContainsKey("PublicityEventAttribute")).ToList() : events;
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
            var startDate = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.AddDays(-(int)CurrentDate.DayOfWeek + 1).Day);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 48 * 3; j += 3)
                {
                    var date = startDate.AddDays(i);
                    date = date.AddMinutes(j * 10);
                    var block = new EmptyBlock(isPublic, date);
                    Grid.SetColumn(block, i);
                    Grid.SetRowSpan(block, 3);
                    Grid.SetRow(block, j);
                    EventsGrid.Children.Add(block);
                }
            }
        }

        private void AddEvent(CalendarEventDto calendarEvent)
        {
            var duration = (calendarEvent.End - calendarEvent.Start).Hours * 6;
            var startColumn = (int)calendarEvent.Start.DayOfWeek == 0 ? 7 : (int)calendarEvent.Start.DayOfWeek - 1;
            var eventBlock = new EventBlock(startColumn, calendarEvent.Start.Hour, duration, calendarEvent);
            Grid.SetRow(eventBlock, eventBlock.StartRow * 6);
            Grid.SetColumn(eventBlock, eventBlock.StartColumn);
            Grid.SetRowSpan(eventBlock, eventBlock.Duration);

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
            UpdateView();
        }

        public void PrevView()
        {
            CurrentDate = CurrentDate.AddDays(-7);
            UpdateView();
        }

        public void UpdateView()
        {
            FillCalendar();
        }
    }
}

