using SchedulePlanner.Application.EventRules;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.CalendarEvents;

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

    public Result<CalendarEvent> ApplyRules()
    {
        var success = eventRuleChecker.Check(calendarEvent, out var failedRule);

        return success ? calendarEvent : Error.Failure($"Правило нарушено: {failedRule?.GetType().Name ?? "None"}");
    }
}