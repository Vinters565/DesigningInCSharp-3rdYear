using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Rules;

public interface IEventRuleChain
{
    public bool Check(CalendarEvent calendarEvent, out string? message);
}