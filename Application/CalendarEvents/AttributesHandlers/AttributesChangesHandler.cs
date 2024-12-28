using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers;

public class AttributesChangesHandler : IAttributesChangesHandler
{
    private readonly IAttributeChangeHandler[] attributeChangeHandlers;

    public AttributesChangesHandler(IAttributeChangeHandler[] attributeChangeHandlers)
    {
        this.attributeChangeHandlers = attributeChangeHandlers;
    }

    public async Task HandleAsync(AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent)
    {
        foreach (var attributeAction in attributeChangeHandlers)
        {
            await attributeAction.HandleAsync(existedAttributes, newAttributes, calendarEvent);
        }
    }
}