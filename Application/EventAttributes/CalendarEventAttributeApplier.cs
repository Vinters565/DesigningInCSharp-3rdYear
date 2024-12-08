using SchedulePlanner.Application.EventRules;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.EventRules;

namespace SchedulePlanner.Application.EventAttributes;

public class CalendarEventAttributeApplier(CalendarEvent calendarEvent, IEventRuleChain eventRuleChain)
{
    public CalendarEventAttributeApplier AddAttribute<T>(T newAttribute) where T : IEventAttribute
    {
        calendarEvent.AddAttribute(newAttribute);
        return this;
    }

    public CalendarEventAttributeApplier RemoveAttribute<T>() where T : IEventAttribute
    {
        calendarEvent.RemoveAttribute<T>();
        return this;
    }

    //TODO: возвращать Result
    public CalendarEvent? Apply()
    {
        var success = eventRuleChain.Check(calendarEvent, out var failedRule);

        var message = success ? "Атрибуты успешно применены" : $"Правило нарушено: {failedRule ?? "none"}";
        Console.WriteLine(message);
        return success ? calendarEvent : null;
    }
}