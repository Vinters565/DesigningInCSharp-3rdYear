using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Application.CalendarEvents.EventAttributes;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Enums;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.Calendars;

public class CalendarService(
    ICalendarEventRepository calendarEventRepository) : ICalendarService
{
    public async Task<Result<List<CalendarEventDto>>> GetPrivateCalendarAsync(Guid userId, DateTime start, CalendarView view)
    {
        // TODO: проверять существование юзера

        // TODO: Данный метод не смотрит на публичность события, соответственно в приватном календаре видно публичные события, норм ли это?
        var end = GetEndDate(start, view);
        var events = await calendarEventRepository.GetAllByUserIdAsync(userId, start, end);

        return events.Select(e => e.ToDto()).ToList();
    }

    public async Task<Result<List<CalendarEventDto>>> GetPublicCalendarByUsernameAsync(string username, DateTime start, CalendarView view)
    {
        // TODO: получаем Id юзера по username
        var userId = Guid.NewGuid();
        
        var end = GetEndDate(start, view);
        var events = await calendarEventRepository.GetPublicByUserIdAsync(userId, start, end);

        return events.Select(e => e.ToDto()).ToList();
    }
    
    private static DateTime GetEndDate(DateTime start, CalendarView view) => 
        start + view.ToTimeSpan();
}