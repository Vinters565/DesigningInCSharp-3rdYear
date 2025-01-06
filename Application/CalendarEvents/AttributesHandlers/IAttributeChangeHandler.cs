using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.ValueTypes;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers;

public interface IAttributeChangeHandler
{
    Task HandleAsync(AttributeData before, AttributeData after, CalendarEvent calendarEvent);
}