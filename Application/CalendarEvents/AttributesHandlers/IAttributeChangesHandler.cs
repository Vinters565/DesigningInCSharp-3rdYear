using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers;

public interface IAttributeChangesHandler
{
    Task HandleAsync(AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent);
}