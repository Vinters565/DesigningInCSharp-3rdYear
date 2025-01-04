using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.EventRules.Rules;

public class NonOverlappingLocationsRule(
    ICalendarEventRepository calendarEventRepository) : IEventRule
{
    public string FailMessage => "Пересечение с другим календарным событием на выбранное место";

    public async Task<bool> CheckAsync(CalendarEvent calendarEvent)
    {
        if (!calendarEvent.AttributeData.TryGetAttribute<DependsOnLocationAttribute>(out var dependsOnLocationAttribute)
            || !dependsOnLocationAttribute!.IsDependsOnLocation)
        {
            return true;
        }
        
        var start = calendarEvent.StartDate;
        var end = calendarEvent.EndDate;
        
        return !await calendarEventRepository.AnyWithLocationAsync(dependsOnLocationAttribute.Location!, start, end);
    }
}