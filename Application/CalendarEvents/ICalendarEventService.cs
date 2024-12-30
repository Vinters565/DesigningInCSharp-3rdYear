using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents;

public interface ICalendarEventService
{
    Task<Result<CalendarEventDto>> GetByIdAsync(Guid id);

    Task<Result<CalendarEventDto>> CreateAsync(
        Guid userId, 
        DateTime start, 
        DateTime end, 
        IReadOnlyDictionary<Type, IEventAttribute> Attributes);
    
    Task<Result<CalendarEventDto>> UpdateAsync(Guid id, UpdateCalendarEventRequest request);
    
    Task<Result> DeleteByIdAsync(Guid id);
}