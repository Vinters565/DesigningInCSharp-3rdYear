using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers;

public class AttributeChangesHandler : IAttributeChangesHandler
{
    private readonly IAttributeChangeHandler[] attributeChangeHandlers;

    public AttributeChangesHandler(IAttributeChangeHandler[] attributeChangeHandlers)
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