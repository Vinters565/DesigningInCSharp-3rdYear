using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.CalendarEvents;

public interface ICalendarEventService
{
    Task<Result<CalendarEventDto>> GetByIdAsync(Guid id);

    Task<Result<CalendarEventDto>> CreateAsync(
        string name,
        Guid userId, 
        DateTime start, 
        DateTime end, 
        IReadOnlyDictionary<Type, IEventAttribute> Attributes);
    
    Task<Result<CalendarEventDto>> UpdateAsync(Guid id, UpdateCalendarEventRequest request);
    
    Task<Result> DeleteByIdAsync(Guid id);
}