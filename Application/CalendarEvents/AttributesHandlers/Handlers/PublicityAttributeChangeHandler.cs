using SchedulePlanner.Application.Subscriptions;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers.Handlers;

public class PublicityAttributeChangeHandler(
    ISubscriptionRepository subscriptionRepository) : IAttributeChangeHandler
{
    public async Task HandleAsync(AttributeData existedAttributes, AttributeData newAttributes, CalendarEvent calendarEvent)
    {
        if (AttributeData.IsAttributeDeleted<PublicityAttribute>(existedAttributes, newAttributes))
            await OnDeleteAsync(existedAttributes, newAttributes, calendarEvent);
    }

    private Task OnDeleteAsync(
        AttributeData existedAttributes, 
        AttributeData newAttributes, 
        CalendarEvent calendarEvent)
    {
        subscriptionRepository.DeleteByCalendarEventId(calendarEvent.Id);
        return Task.CompletedTask;
    } 
}