using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.EventRules;

public interface IEventRuleChain
{
    public bool Check(CalendarEvent calendarEvent, out string? message);
}