using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Domain.Enums;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.Calendars;

public interface ICalendarService
{
    Task<Result<List<CalendarEventDto>>> GetPrivateCalendarAsync(Guid userId, DateTime start, CalendarView view);

    Task<Result<List<CalendarEventDto>>> GetPublicCalendarByUsernameAsync(
        string username, 
        DateTime start,
        CalendarView view);
}