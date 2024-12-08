using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Interfaces;

public interface ICalendarEventRepository
{
    public CalendarEvent[] GetEvents(DateTime start, DateTime end);

    public bool Any(DateTime start, DateTime end);
}