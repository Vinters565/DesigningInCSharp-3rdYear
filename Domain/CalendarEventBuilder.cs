using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain;

public class CalendarEventBuilder
{
    public CalendarEvent CalendarEvent { get; }

    public CalendarEventBuilder(CalendarEvent calendarEvent)
    {
        CalendarEvent = calendarEvent;
    }

    public CalendarEventBuilder AddAttribute<T>(T newAttribute) where T : CalendarEventAttribute
    {
        CalendarEvent.AddAttribute(newAttribute);
        return this;
    }

    public CalendarEventBuilder RemoveAttribute<T>() where T : CalendarEventAttribute
    {
        CalendarEvent.RemoveAttribute<T>();
        return this;
    }

    public CalendarEvent? Apply()
    {
        // Валидация события
        var success = RuleManager.TryCheckEvent(CalendarEvent, out var message);

        Console.WriteLine(message);
        return success ? CalendarEvent : null;
    }
}