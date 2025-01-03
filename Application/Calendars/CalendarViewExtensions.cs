using SchedulePlanner.Domain.Enums;

namespace SchedulePlanner.Application.Calendars;

public static class CalendarViewExtensions
{
    public static TimeSpan ToTimeSpan(this CalendarView calendarView)
    {
        return calendarView switch
        {
            CalendarView.Day => TimeSpan.FromDays(1),
            CalendarView.Week => TimeSpan.FromDays(7),
            CalendarView.Month => TimeSpan.FromDays(30),
            CalendarView.Year => TimeSpan.FromDays(365),
            _ => throw new ArgumentOutOfRangeException(nameof(calendarView), calendarView, null)
        };
    }
}