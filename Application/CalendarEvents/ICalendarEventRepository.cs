using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents;

public interface ICalendarEventRepository
{
    Task<List<CalendarEvent>> GetAllByUserIdAsync(Guid userId, DateTime start, DateTime end);
    
    Task<List<CalendarEvent>> GetPublicByUserIdAsync(Guid userId, DateTime start, DateTime end);

    Task<CalendarEvent?> GetByIdAsync(Guid id);

    void Delete(CalendarEvent calendarEvent);
    
    public Task AddEventAsync(CalendarEvent newEvent);
    public Task<List<CalendarEvent>> GetAllEventsAsync();
    public void UpdateEvent(CalendarEvent updatedEvent);
    public void DeleteEventById(string id);
    public Task<List<CalendarEvent>> GetEventsAsync(DateTime start, DateTime end);

    public Task<bool> AnyAsync(DateTime start, DateTime end);

    public Task<bool> AnySinglOnlyAsync(DateTime start, DateTime end);

    public Task<bool> AnyWithLocationAsync(string location, DateTime start, DateTime end);

    public Task SaveChangesAsync();
}
