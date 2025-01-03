using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Interfaces;

public interface IEventRule
{
    string FailMessage { get; }
    
    Task<bool> CheckAsync(CalendarEvent calendarEvent);
}