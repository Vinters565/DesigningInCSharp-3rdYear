using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.UserControls
{
    public partial class MonthView : UserControl, IViewCalendar
    {
        private readonly TextBlock dateTextBlock;

        public DateTime CurrentDate { get; set; }

        public MonthView(TextBlock dateTextBlock)
        {
            InitializeComponent();
            this.dateTextBlock = dateTextBlock;
            CurrentDate = DateTime.Now;
            UpdateView();
        }

        private void LoadCalendar()
        {
            ClearOldCells();
            FillPreviousMonthDays();
            FillCurrentMonthDays();
            FillNextMonthDays();
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

        private void FillPreviousMonthDays()
        {
            var firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            startDay = startDay == 0 ? 6 : startDay - 1;

            var prevMonthDays = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month == 1 ? 12 : CurrentDate.Month - 1);

            for (int i = 0; i < startDay; i++)
            {
                var dayEmptyBlock = new EmptyBlock();
                var prevDay = prevMonthDays - startDay + 1 + i;

                var dayBlock = CreateDayTextBlock(prevDay.ToString(), Brushes.DarkGray);
                dayEmptyBlock.GridElementRearward.Children.Add(dayBlock);

                Grid.SetRow(dayEmptyBlock, 1);
                Grid.SetColumn(dayEmptyBlock, i);
                MonthCalendar.Children.Add(dayEmptyBlock);
            }
        }

        private void FillCurrentMonthDays()
        {
            var daysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            var firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            startDay = startDay == 0 ? 6 : startDay - 1;

            for (int day = 1; day <= daysInMonth; day++)
            {
                var dayEmptyBlock = new EmptyBlock();
                var dayBlock = CreateDayTextBlock(day.ToString(), Brushes.White);
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

        private void FillNextMonthDays()
        {
            var firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            startDay = startDay == 0 ? 6 : startDay - 1;

            var nextDaysToShow = 42 - (startDay + daysInMonth);
            for (int day = 1; day <= nextDaysToShow; day++)
            {
                var dayEmptyBlock = new EmptyBlock();
                var dayBlock = CreateDayTextBlock(day.ToString(), Brushes.DarkGray);
                dayEmptyBlock.GridElementRearward.Children.Add(dayBlock);

                var column = (startDay + daysInMonth + day - 1) % 7;
                var row = (startDay + daysInMonth + day - 1) / 7 + 1;
                Grid.SetRow(dayEmptyBlock, row);
                Grid.SetColumn(dayEmptyBlock, column);
                MonthCalendar.Children.Add(dayEmptyBlock);
            }
        }

        private TextBlock CreateDayTextBlock(string text, Brush foreground)
        {
            return new TextBlock
            {
                Text = text,
                Background = Brushes.Transparent,
                Foreground = foreground,
                Margin = new Thickness(3)
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