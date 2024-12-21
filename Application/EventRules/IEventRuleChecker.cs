using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventRules;

namespace SchedulePlanner.Application.EventRules;

public interface IEventRuleChecker
{
    Task<IEventRule?> CheckAsync(CalendarEvent calendarEvent);
}