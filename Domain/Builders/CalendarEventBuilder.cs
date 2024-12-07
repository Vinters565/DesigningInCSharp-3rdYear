using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Entities.CalendarEventAttributes;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Domain.Rules;

namespace SchedulePlanner.Domain.Builders;

public class CalendarEventBuilder(CalendarEvent calendarEvent)
{
    public CalendarEventBuilder AddAttribute<T>(T newAttribute) where T : CalendarEventAttribute
    {
        calendarEvent.AddAttribute(newAttribute);
        return this;
    }

    public CalendarEventBuilder RemoveAttribute<T>() where T : CalendarEventAttribute
    {
        calendarEvent.RemoveAttribute<T>();
        return this;
    }

    public CalendarEvent? Apply()
    {
        // Валидация события
        var success = GetRuleChain().Check(calendarEvent, out var failedRule);

        var message = success ? "Правила совместимы" : $"Несовместимое правило: {failedRule ?? "none"}";
        Console.WriteLine(message);
        return success ? calendarEvent : null;
    }
    
    // TODO: брать цепочку из DI
    private static IEventRuleChain GetRuleChain()
    {
        var ruleChain = new EventRuleChain()
            .AddNextEventRule(new MandatoryEventRule())
            .AddNextEventRule(new TimeEventRule())
            .AddNextEventRule(new SingleOnlyEventRule(new CalendarEventRepository()));

        return ruleChain;
    }
}