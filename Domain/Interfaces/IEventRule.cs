using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Interfaces;

public interface IEventRule
{
    public int Priority { get; }

    public Task<bool> CheckAsync(CalendarEvent calendarEvent);
}