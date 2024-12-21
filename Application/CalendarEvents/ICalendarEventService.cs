using SchedulePlanner.Application.Dto;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.CalendarEvents;

public interface ICalendarEventService
{
    Task<Result<List<CalendarEventDto>>> GetByUserIdAsync(Guid userId, DateTime start, DateTime end);

    Task<Result<CalendarEventDto>> CreateAsync(Guid userId, DateTime start, DateTime end,
        Dictionary<Type, IEventAttribute> attributes);
}