using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Application.CalendarEvents.EventAttributes;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Enums;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.Calendars;

public class CalendarService(
    IEventAttributeManager eventAttributeManager,
    ICalendarEventRepository calendarEventRepository) : ICalendarService
{
    public async Task<Result<List<CalendarEventDto>>> GetPrivateCalendarAsync(Guid userId, DateTime start, CalendarView view)
    {
        //TODO: проверять существование юзера

        //TODO: Данный метод не смотрит на публичность события, соответственно в приватном календаре видно публичные события, норм ли это?
        var end = start + view.ToTimeSpan();
        var events = await calendarEventRepository.GetAllByUserIdAsync(userId, start, end);

        return events.Select(e => e.ToDto()).ToList();
    }

    public async Task<Result<CalendarEventDto>> AddPrivateCalendarEventAsync(Guid userId, DateTime start, DateTime end, IReadOnlyDictionary<Type, IEventAttribute> attributes)
    {
        //TODO: проверять существование юзера
        var calendarEvent = new CalendarEvent(userId, start, end);

        var attributesResult = await eventAttributeManager.UpdateAsync(calendarEvent, attributes.ToDictionary());
        if (attributesResult.IsError) return attributesResult.Error;

        calendarEventRepository.AddEvent(calendarEvent);
        return await Task.FromResult(calendarEvent.ToDto());
    }
}