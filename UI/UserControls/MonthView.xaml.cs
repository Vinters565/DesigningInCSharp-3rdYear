using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.UserControls
{
    public partial class MonthView : UserControl
    {
        private DateTime currentDate;

        public MonthView()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
            UpdateCalendar();
        }

        private void UpdateCalendar()
        {
            MonthTextBlock.Text = currentDate.ToString("MMMM yyyy");
            LoadCalendar();
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
            for (int i = 2; i < MonthCalendar.RowDefinitions.Count; i++)
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
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            startDay = startDay == 0 ? 6 : startDay - 1;

            var prevMonthDays = DateTime.DaysInMonth(currentDate.Year, currentDate.Month == 1 ? 12 : currentDate.Month - 1);

            for (int i = 0; i < startDay; i++)
            {
                var dayBorder = CreateDayBorder();
                var prevDay = prevMonthDays - startDay + 1 + i;

                var dayBlock = CreateDayTextBlock(prevDay.ToString(), Brushes.DarkGray);
                dayBorder.Child = dayBlock;

                Grid.SetRow(dayBorder, 2);
                Grid.SetColumn(dayBorder, i);
                MonthCalendar.Children.Add(dayBorder);
            }
        }

        private void FillCurrentMonthDays()
        {
            var daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            startDay = startDay == 0 ? 6 : startDay - 1;

            for (int day = 1; day <= daysInMonth; day++)
            {
                var dayBorder = CreateDayBorder();
                var dayBlock = CreateDayTextBlock(day.ToString(), Brushes.White);
                dayBorder.Child = dayBlock;

                var column = (startDay + day - 1) % 7;
                var row = (startDay + day - 1) / 7 + 2;
                Grid.SetRow(dayBorder, row);
                Grid.SetColumn(dayBorder, column);
                MonthCalendar.Children.Add(dayBorder);
            }
        }

        private void FillNextMonthDays()
        {
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            startDay = startDay == 0 ? 6 : startDay - 1;

            var nextDaysToShow = 42 - (startDay + daysInMonth);
            for (int day = 1; day <= nextDaysToShow; day++)
            {
                var dayBorder = CreateDayBorder();
                var dayBlock = CreateDayTextBlock(day.ToString(), Brushes.DarkGray);
                dayBorder.Child = dayBlock;

                var column = (startDay + daysInMonth + day - 1) % 7;
                var row = (startDay + daysInMonth + day - 1) / 7 + 2;
                Grid.SetRow(dayBorder, row);
                Grid.SetColumn(dayBorder, column);
                MonthCalendar.Children.Add(dayBorder);
            }
        }

        private Border CreateDayBorder()
        {
            return new Border
            {
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(1)
            };
        }

        private TextBlock CreateDayTextBlock(string text, Brush foreground)
        {
            return new TextBlock
            {
                Text = text,
                Background = Brushes.Transparent,
                Foreground = foreground
            };
        }

        private void PreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(-1);
            UpdateCalendar();
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(1);
            UpdateCalendar();
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
    }
}