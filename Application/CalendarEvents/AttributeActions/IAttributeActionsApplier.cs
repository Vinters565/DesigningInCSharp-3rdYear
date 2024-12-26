using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents.AttributeActions;

public interface IAttributeActionsApplier
{
    Task Apply(AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent);
}