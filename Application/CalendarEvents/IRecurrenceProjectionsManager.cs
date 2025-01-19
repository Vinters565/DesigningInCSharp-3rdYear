using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents;

public interface IRecurrenceProjectionsManager
{
    List<CalendarEvent> GetProjections(IReadOnlyCollection<CalendarEvent> recurringEvents, DateTime maxUntil);
}