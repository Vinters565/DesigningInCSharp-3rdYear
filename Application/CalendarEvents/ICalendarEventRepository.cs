using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents;

public interface ICalendarEventRepository
{
    Task<List<CalendarEvent>> GetAllByUserIdAsync(Guid userId, DateTime start, DateTime end);
    
    Task<List<CalendarEvent>> GetPublicByUserIdAsync(Guid userId, DateTime start, DateTime end);

    Task<CalendarEvent?> GetByIdAsync(Guid id);
    
    void Create(CalendarEvent newEvent);
    
    void Delete(CalendarEvent calendarEvent);

    Task<bool> AnyAsync(Guid userId, DateTime start, DateTime end);

    Task<bool> AnySingleOnlyAsync(Guid userId, DateTime start, DateTime end);

    Task<bool> AnyWithLocationAsync(Guid userId, string location, DateTime start, DateTime end);

    Task SaveChangesAsync();
}
