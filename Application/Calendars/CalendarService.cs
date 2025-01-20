using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Application.Users;
using SchedulePlanner.Domain.Enums;
using SchedulePlanner.Domain.EventAttributes.Attributes;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.Calendars;

public class CalendarService(
    ISubscribedCalendarEventService subscribedCalendarEventService,
    IRecurrenceProjectionsManager recurrenceProjectionsManager,
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

        var projections = recurrenceProjectionsManager.GetProjections(
            events.Where(e => e.AttributeData.HasActiveAttribute<RecurrenceEventAttribute>()).ToList(), end);

        var dtos = events.Select(e => e.ToDto()).ToList();
        dtos.AddRange(projections.Select(p => p.ToDto()));
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