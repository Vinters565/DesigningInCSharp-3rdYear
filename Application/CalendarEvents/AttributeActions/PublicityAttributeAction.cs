using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.CalendarEvents.AttributeActions;

public class PublicityAttributeAction : IAttributeAction
{
    public async Task ProcessAsync(AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent)
    {
        if (AttributeData.IsAttributeDeleted<PublicityAttribute>(existedAttributes, newAttributes))
            await OnDeleteAsync(existedAttributes, newAttributes, calendarEvent);
    }

    private async Task OnDeleteAsync(
        AttributeData existedAttributes, 
        AttributeData newAttributes, 
        CalendarEvent calendarEvent)
    {
        //TODO: удалять записи в таблице Subscriptions по данному событию
        return;
    } 
}