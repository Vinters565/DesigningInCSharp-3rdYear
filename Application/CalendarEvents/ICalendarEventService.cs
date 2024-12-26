using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Domain.Common.Results;

namespace SchedulePlanner.Application.CalendarEvents;

public interface ICalendarEventService
{
    Task<Result<CalendarEventDto>> GetByIdAsync(Guid id);

    Task<Result<CalendarEventDto>> CreateAsync(Guid userId, CreateCalendarEventRequest request);
    
    Task<Result<CalendarEventDto>> UpdateAsync(Guid id, UpdateCalendarEventRequest request);
    
    Task<Result> DeleteByIdAsync(Guid id);
}