using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.ValueTypes;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers;

public interface IAttributeChangesHandler
{
    Task HandleAsync(AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent);
}