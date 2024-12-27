using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Application.CalendarEvents.EventAttributes;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents;

public class CalendarEventService(
    IEventAttributeManager eventAttributeManager,
    ICalendarEventRepository calendarEventRepository) : ICalendarEventService
{
    public async Task<Result<CalendarEventDto>> GetByIdAsync(Guid id)
    {
        var calendarEvent = await calendarEventRepository.GetByIdAsync(id);
        if (calendarEvent == null) return Error.NotFound("Календарное событие не найдено");
        
        return calendarEvent.ToDto();
    }

    public async Task<Result<CalendarEventDto>> CreateAsync(Guid userId, DateTime start, DateTime end,
        IReadOnlyDictionary<Type, IEventAttribute> attributes)
    {
        //TODO: проверять существование юзера
        var calendarEvent = new CalendarEvent(userId, start, end);

        var attributesResult = await eventAttributeManager.UpdateAsync(calendarEvent, attributes.ToDictionary());
        if (attributesResult.IsError) return attributesResult.Error;

        calendarEventRepository.AddEvent(calendarEvent);
        return await Task.FromResult(calendarEvent.ToDto());
    }

    public async Task<Result<CalendarEventDto>> UpdateAsync(Guid id, UpdateCalendarEventRequest request)
    {
        var calendarEvent = await calendarEventRepository.GetByIdAsync(id);
        if (calendarEvent == null) return Error.NotFound("Календарное событие не найдено");

        calendarEvent.Update(request.Start, request.End);

        if (request.Attributes != null)
        {
            var attributesResult = await eventAttributeManager.UpdateAsync(calendarEvent, request.Attributes.ToDictionary());
            if (attributesResult.IsError) return attributesResult.Error;
        }

        calendarEventRepository.UpdateEvent(calendarEvent);

        return calendarEvent.ToDto();
    }

    public async Task<Result> DeleteByIdAsync(Guid id)
    {
        var calendarEvent = await calendarEventRepository.GetByIdAsync(id);
        if (calendarEvent == null) return Error.NotFound("Календарное событие не найдено");
        
        calendarEventRepository.Delete(calendarEvent);
        return Result.Success();
    }
}