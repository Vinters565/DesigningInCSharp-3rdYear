using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.ValueTypes;
using SchedulePlanner.Utils.Extensions;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers;

public class AttributeChangesHandler(
    IAttributeChangeHandler[] attributeChangeHandlers) : IAttributeChangesHandler
{
    public async Task HandleAsync(
        AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent)
    {
        await attributeChangeHandlers.ForEachAsync(handler =>
            handler.HandleAsync(existedAttributes, newAttributes, calendarEvent));
    }
}