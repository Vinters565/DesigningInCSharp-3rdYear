using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.EventRules;

public interface IEventRuleChecker
{
    Task<IEventRule?> CheckAsync(CalendarEvent calendarEvent);
}