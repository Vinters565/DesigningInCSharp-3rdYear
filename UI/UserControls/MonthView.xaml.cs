using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using UI.Dto;

namespace UI.UserControls
{
    public partial class MonthView : UserControl, IViewCalendar
    {
        private readonly TextBlock dateTextBlock;
        private readonly bool isPublic;
        private readonly string? userName;

        public DateTime CurrentDate { get; set; }

        public MonthView(TextBlock dateTextBlock, bool isPublic)
        {
            InitializeComponent();
            this.dateTextBlock = dateTextBlock;
            CurrentDate = DateTime.Now;
            this.isPublic = isPublic;
            UpdateView();
        }

        public MonthView(TextBlock dateTextBlock, string userName)
        {
            InitializeComponent();
            this.dateTextBlock = dateTextBlock;
            CurrentDate = DateTime.Now;
            isPublic = true;
            this.userName = userName;
            UpdateView();
        }

        private async void LoadCalendar()
        {
            ClearOldCells();
            var events = await LoadEvents();
            FillPreviousMonthDays(events);
            FillCurrentMonthDays(events);
            FillNextMonthDays(events);
        }

        private async Task<List<CalendarEventDto>> LoadEvents()
        {
            var startDate = new DateTime(
                    CurrentDate.Month == 1 ? CurrentDate.Year - 1 : CurrentDate.Year,
                    CurrentDate.Month == 1 ? 12 : CurrentDate.Month - 1,
                    21);
            var events = userName != null && isPublic
                ? await App.ServiceProvider.GetRequiredService<ApiClient>().GetPublicEventsAsync(userName, startDate, 3)
                : await App.ServiceProvider.GetRequiredService<ApiClient>().GetPrivateEventsAsync(startDate, 3);
            return isPublic ? events.Where(e => e.Attributes.ContainsKey("PublicityEventAttribute")).ToList() : events;
        }

        private void ClearOldCells()
        {
            for (int i = 1; i < MonthCalendar.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    var child = GetChildAt(MonthCalendar, i, j);
                    if (child != null)
                    {
                        MonthCalendar.Children.Remove(child);
                    }
                }
            }
        }

        private void FillPreviousMonthDays(List<CalendarEventDto> events)
        {
            var firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            startDay = startDay == 0 ? 6 : startDay - 1;

            var prevMonthDays = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month == 1 ? 12 : CurrentDate.Month - 1);

            for (int i = 0; i < startDay; i++)
            {
                var prevDate = new DateTime(
                    CurrentDate.Month == 1 ? CurrentDate.Year - 1 : CurrentDate.Year,
                    CurrentDate.Month == 1 ? 12 : CurrentDate.Month - 1,
                    prevMonthDays - startDay + 1 + i);
                var text = $"{prevDate.Day} \n {GetEventsByDate(prevDate, events)}";
                var dayEmptyBlock = new EmptyBlock(isPublic, prevDate);
                var dayBlock = CreateDayTextBlock(text, Brushes.DarkGray);
                dayEmptyBlock.GridElementRearward.Children.Add(dayBlock);
                
                Grid.SetRow(dayEmptyBlock, 1);
                Grid.SetColumn(dayEmptyBlock, i);
                MonthCalendar.Children.Add(dayEmptyBlock);
            }
        }

        private void FillCurrentMonthDays(List<CalendarEventDto> events)
        {
            var daysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            var firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            startDay = startDay == 0 ? 6 : startDay - 1;

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(CurrentDate.Year, CurrentDate.Month, day);
                var dayEmptyBlock = new EmptyBlock(isPublic, date);
                var text = $"{day.ToString()} \n {GetEventsByDate(date, events)}";
                var dayBlock = CreateDayTextBlock(text, Brushes.White);
                if (day == CurrentDate.Day && CurrentDate.Month == DateTime.Now.Month)
                {
                    dayEmptyBlock.Background = Brushes.Brown;
                }
                dayEmptyBlock.GridElementRearward.Children.Add(dayBlock);

                var column = (startDay + day - 1) % 7;
                var row = (startDay + day - 1) / 7 + 1;
                Grid.SetRow(dayEmptyBlock, row);
                Grid.SetColumn(dayEmptyBlock, column);
                MonthCalendar.Children.Add(dayEmptyBlock);
            }
        }

        private void FillNextMonthDays(List<CalendarEventDto> events)
        {
            var firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            startDay = startDay == 0 ? 6 : startDay - 1;

            var nextDaysToShow = 42 - (startDay + daysInMonth);
            for (int day = 1; day <= nextDaysToShow; day++)
            {
                var nextDate = new DateTime(
                    CurrentDate.Month == 12 ? CurrentDate.Year + 1 : CurrentDate.Year,
                    CurrentDate.Month == 12 ? 1 : CurrentDate.Month + 1,
                    day);
                var text = $"{day} \n {GetEventsByDate(nextDate, events)}";
                var dayEmptyBlock = new EmptyBlock(isPublic, nextDate);
                var dayBlock = CreateDayTextBlock(text, Brushes.DarkGray);
                dayEmptyBlock.GridElementRearward.Children.Add(dayBlock);

                var column = (startDay + daysInMonth + day - 1) % 7;
                var row = (startDay + daysInMonth + day - 1) / 7 + 1;
                Grid.SetRow(dayEmptyBlock, row);
                Grid.SetColumn(dayEmptyBlock, column);
                MonthCalendar.Children.Add(dayEmptyBlock);
            }
        }

        private string GetEventsByDate(DateTime date, List<CalendarEventDto> events)
        {
            var count = events
                .Where(x => x.Start.Day == date.Day && x.Start.Month == date.Month && x.Start.Year == date.Year)
                .Count();
            return count > 0 ? $"Событий в этот день: {count}" : "";
        }

        private TextBlock CreateDayTextBlock(string text, Brush foreground)
        {
            return new TextBlock
            {
                Text = text,
                Background = Brushes.Transparent,
                Foreground = foreground,
                Margin = new Thickness(3),
                TextWrapping = TextWrapping.WrapWithOverflow
            };
        }

        private void PreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            PrevView();
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            NextView();
        }

        private UIElement GetChildAt(Grid grid, int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                {
                    return child;
                }
            }
            return null;
        }

        public void NextView()
        {
            CurrentDate = CurrentDate.AddMonths(1);
            UpdateView();
        }

        public void PrevView()
        {
            CurrentDate = CurrentDate.AddMonths(-1);
            UpdateView();
        }

        public void UpdateView()
        {
            dateTextBlock.Text = CurrentDate.ToString("MMMM yyyy");
            LoadCalendar();
        }
    }
}