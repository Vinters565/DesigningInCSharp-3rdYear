using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Application.CalendarEvents.EventAttributes;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Enums;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.Calendars;

public class CalendarService(
    IUserRepository userRepository,
    ICalendarEventRepository calendarEventRepository) : ICalendarService
{
    public async Task<Result<List<CalendarEventDto>>> GetPrivateCalendarAsync(Guid userId, DateTime start, CalendarView view)
    {
        var user = await userRepository.GetByIDAsync(userId);
        if (user == null) return Error.NotFound("User not found");

        // TODO: Данный метод не смотрит на публичность события, соответственно в приватном календаре видно публичные события, норм ли это?
        var end = GetEndDate(start, view);
        var events = await calendarEventRepository.GetAllByUserIdAsync(userId, start, end);

        return events.Select(e => e.ToDto()).ToList();
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