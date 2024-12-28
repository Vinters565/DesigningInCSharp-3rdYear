using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers;

public interface IAttributesChangesHandler
{
    Task HandleAsync(AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent);
}