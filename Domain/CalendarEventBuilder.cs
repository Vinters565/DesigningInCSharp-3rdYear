using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.EventRules;

namespace SchedulePlanner.Domain;

public class CalendarEventBuilder(CalendarEvent calendarEvent, IEventRuleChain eventRuleChain)
{
    public CalendarEventBuilder AddAttribute<T>(T newAttribute) where T : IEventAttribute
    {
        calendarEvent.AddAttribute(newAttribute);
        return this;
    }

    public CalendarEventBuilder RemoveAttribute<T>() where T : IEventAttribute
    {
        calendarEvent.RemoveAttribute<T>();
        return this;
    }

    public CalendarEvent? Apply()
    {
        // Валидация события
        var success = eventRuleChain.Check(calendarEvent, out var failedRule);

        var message = success ? "Правила совместимы" : $"Несовместимое правило: {failedRule ?? "none"}";
        Console.WriteLine(message);
        return success ? calendarEvent : null;
    }
}