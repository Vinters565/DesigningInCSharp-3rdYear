using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.EventRules.Rules;

public class NonOverlappingLocationsRule(
    ICalendarEventRepository calendarEventRepository) : IEventRule
{
    public async Task<bool> CheckAsync(CalendarEvent calendarEvent)
    {
        if (!calendarEvent.AttributeData.TryGetAttribute<DependsOnLocationAttribute>(out var dependsOnLocationAttribute)
            || !dependsOnLocationAttribute!.IsDependsOnLocation)
        {
            return true;
        }
        var userId = calendarEvent.UserId;
        var start = calendarEvent.StartDate;
        var end = calendarEvent.EndDate;
        
        return !await calendarEventRepository.AnyWithLocationAsync(userId, dependsOnLocationAttribute.Location!, start, end);
    }
}