using SchedulePlanner.Domain.Entities;
using System.Collections.Generic;

namespace SchedulePlanner.Domain.Interfaces;

public interface ICalendarEventRepository
{
    Task<List<CalendarEvent>> GetByUserIdAsync(Guid userId);
    public void AddEvent(CalendarEvent newEvent);
    public List<CalendarEvent> GetAllEvents();
    public void UpdateEvent(CalendarEvent updatedEvent);
    public void DeleteEvent(string id);
    public List<CalendarEvent> GetEvents(DateTime start, DateTime end);

    public bool Any(DateTime start, DateTime end);
}