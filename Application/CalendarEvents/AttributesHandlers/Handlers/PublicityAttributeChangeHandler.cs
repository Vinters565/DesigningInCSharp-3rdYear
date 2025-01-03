using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers.Handlers;

public class PublicityAttributeChangeHandler : IAttributeChangeHandler
{
    public async Task HandleAsync(AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent)
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