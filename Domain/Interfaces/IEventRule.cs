using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Interfaces;

public interface IEventRule
{
    public Task<bool> CheckAsync(CalendarEvent calendarEvent);
}