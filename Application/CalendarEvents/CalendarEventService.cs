using SchedulePlanner.Application.Dto;
using SchedulePlanner.Application.EventRules;
using SchedulePlanner.Domain.Common.Results;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents;

public class CalendarEventService(
    IEventRuleChecker ruleChecker,
    ICalendarEventRepository calendarEventRepository) : ICalendarEventService
{
    public async Task<Result<List<CalendarEventDto>>> GetByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        //TODO: проверять существование юзера
        var events = await calendarEventRepository.GetByUserIdAsync(userId);

        return events.Select(e => e.ToDto()).ToList();
    }

    public async Task<Result<CalendarEventDto>> CreateAsync(Guid userId, DateTime start, DateTime end,
        Dictionary<Type, IEventAttribute> attributes)
    {
        //TODO: проверять существование юзера
        var calendarEvent = new CalendarEvent(userId, start, end, attributes);
        var isEventValid = ruleChecker.Check(calendarEvent, out var failedRule);

        if (!isEventValid)
        {
            return Error.Failure($"Правило '{failedRule}' нарушено");
        }

        calendarEventRepository.AddEvent(calendarEvent);
        return await Task.FromResult(calendarEvent.ToDto());
    }
}