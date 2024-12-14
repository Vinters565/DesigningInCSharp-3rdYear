using SchedulePlanner.Application.Dto;
using SchedulePlanner.Domain.Common.Results;

namespace SchedulePlanner.Application.CalendarEvents;

public class CalendarEventService : ICalendarEventService
{
    public Task<Result<CalendarEventDto>> CreateAsync(Guid userId, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }
}