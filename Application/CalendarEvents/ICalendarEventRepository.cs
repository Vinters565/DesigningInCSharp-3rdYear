using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents;

public interface ICalendarEventRepository
{
    Task<List<CalendarEvent>> GetAllByUserIdAsync(Guid userId, DateTime start, DateTime end);
    
    Task<List<CalendarEvent>> GetPublicByUserIdAsync(Guid userId, DateTime start, DateTime end);

    Task<CalendarEvent?> GetByIdAsync(Guid id);

    void Delete(CalendarEvent calendarEvent);
    
    public void AddEvent(CalendarEvent newEvent);
    public List<CalendarEvent> GetAllEvents();
    public void UpdateEvent(CalendarEvent updatedEvent);
    public void DeleteEvent(string id);
    public List<CalendarEvent> GetEvents(DateTime start, DateTime end);

    public bool Any(DateTime start, DateTime end);

    public Task<bool> AnyWithLocationAsync(string location, DateTime start, DateTime end);
}
