using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.EventRules;

public interface IEventRuleChecker
{
    public bool Check(CalendarEvent calendarEvent, out string? message);
}