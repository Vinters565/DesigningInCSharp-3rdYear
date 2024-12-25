using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.EventRules.Rules;

public class SingleOnlyEventRule(ICalendarEventRepository calendarEventRepository) : IEventRule
{
    public int Priority => 2;

    public async Task<bool> CheckAsync(CalendarEvent calendarEvent)
    {
        if (!calendarEvent.TryGetAttribute<SingleOnlyEventAttribute>(out var singleOnlyEventAttribute) 
            || !singleOnlyEventAttribute!.IsSingleOnly)
        {
            return true;
        }

        var start = calendarEvent.StartDate;
        var end = calendarEvent.EndDate;

        return await Task.FromResult(!calendarEventRepository.Any(start, end));
    }
}