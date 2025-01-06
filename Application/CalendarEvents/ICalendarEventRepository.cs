using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents;

public interface ICalendarEventRepository
{
    Task<List<CalendarEvent>> GetByUserIdAsync(Guid userId, DateTime start, DateTime end);
    
    Task<List<CalendarEvent>> GetPublicByUserIdAsync(Guid userId, DateTime start, DateTime end);
    
    Task<CalendarEvent?> GetByIdAsync(Guid id);
    
    Task<List<CalendarEvent>> GetByIdsAsync(IReadOnlyCollection<Guid> ids);
    
    void Create(CalendarEvent newEvent);
    
    void Delete(CalendarEvent calendarEvent);

    Task<bool> AnyAsync(Guid userId, DateTime start, DateTime end);

    Task<bool> AnySingleOnlyAsync(Guid userId, DateTime start, DateTime end);

    Task<bool> AnyWithLocationAsync(string location, DateTime start, DateTime end);

    Task SaveChangesAsync();
}
