using SchedulePlanner.Application.Dto;
using SchedulePlanner.Domain.Common.Results;

namespace SchedulePlanner.Application.CalendarEvents;

public interface ICalendarEventService
{
    Task<Result<CalendarEventDto>> CreateAsync(Guid userId, DateTime start, DateTime end);
}