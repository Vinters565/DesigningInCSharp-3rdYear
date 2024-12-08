using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.EventRules;

public interface IEventRuleChain
{
    public bool Check(CalendarEvent calendarEvent, out string? message);
}