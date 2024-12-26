using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents.AttributeActions;

public class AttributeActionsApplier : IAttributeActionsApplier
{
    private readonly IAttributeAction[] attributeActions;

    public AttributeActionsApplier(IAttributeAction[] attributeActions)
    {
        this.attributeActions = attributeActions;
    }

    public async Task Apply(AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent)
    {
        foreach (var attributeAction in attributeActions)
        {
            await attributeAction.ProcessAsync(existedAttributes, newAttributes, calendarEvent);
        }
    }
}