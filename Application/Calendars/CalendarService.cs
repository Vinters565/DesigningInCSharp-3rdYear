using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Domain.Enums;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.Calendars;

public class CalendarService(
    ISubscribedCalendarEventService subscribedCalendarEventService,
    IUserRepository userRepository,
    ICalendarEventRepository calendarEventRepository) : ICalendarService
{
    public async Task<Result<List<CalendarEventDto>>> GetPrivateCalendarAsync(Guid userId, DateTime start,
        CalendarView view)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null) return Error.NotFound("User not found");
 
        var end = GetEndDate(start, view);
        var events = await calendarEventRepository.GetByUserIdAsync(userId, start, end);

        var dtos = events.Select(e => e.ToDto()).ToList();
        dtos.AddRange(await subscribedCalendarEventService.GetByUserAsync(user, start, end));

        return dtos;
    }

    public async Task<Result<List<CalendarEventDto>>> GetPublicCalendarByUsernameAsync(string username, DateTime start, CalendarView view)
    {
        var user = await userRepository.GetByUsernameAsync(username);
        if (user == null) return Error.NotFound("User not found");
        
        var end = GetEndDate(start, view);
        var events = await calendarEventRepository.GetPublicByUserIdAsync(user.Id, start, end);

        return events.Select(e => e.ToDto()).ToList();
    }
    
    private static DateTime GetEndDate(DateTime start, CalendarView view) => 
        start + view.ToTimeSpan();
}