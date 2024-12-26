using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Enums;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.Calendars;

public interface ICalendarService
{
    Task<Result<List<CalendarEventDto>>> GetPrivateCalendarAsync(Guid userId, DateTime start, CalendarView view);

    Task<Result<CalendarEventDto>> AddPrivateCalendarEventAsync(
        Guid userId, 
        DateTime start, 
        DateTime end, 
        IReadOnlyDictionary<Type, IEventAttribute> Attributes);
}