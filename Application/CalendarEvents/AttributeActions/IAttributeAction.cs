using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents.AttributeActions;

public interface IAttributeAction
{
    Task ProcessAsync(AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent);
}