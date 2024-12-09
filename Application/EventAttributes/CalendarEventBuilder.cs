using SchedulePlanner.Application.EventRules;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.EventAttributes;

public class CalendarEventBuilder(CalendarEvent calendarEvent, IEventRuleChecker eventRuleChecker)
{
    public CalendarEventBuilder AddAttribute<T>(T attribute) where T : IEventAttribute
    {
        calendarEvent.AddAttribute(attribute);
        return this;
    }

    public CalendarEventBuilder WithAttribute<T>(T attribute) where T : IEventAttribute
    {
        calendarEvent.UpdateAttribute(attribute);
        return this;
    }

    public CalendarEventBuilder RemoveAttribute<T>() where T : IEventAttribute
    {
        calendarEvent.RemoveAttribute<T>();
        return this;
    }

    //TODO: возвращать Result
    public CalendarEvent? ApplyRules()
    {
        var success = eventRuleChecker.Check(calendarEvent, out var failedRule);

        var message = success ? "Атрибуты успешно применены" : $"Правило нарушено: {failedRule ?? "none"}";
        Console.WriteLine(message);
        return success ? calendarEvent : null;
    }
}