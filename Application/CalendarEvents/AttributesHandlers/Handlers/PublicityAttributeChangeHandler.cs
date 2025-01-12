using SchedulePlanner.Application.Subscriptions;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.EventAttributes.Attributes;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers.Handlers;

public class PublicityAttributeChangeHandler(
    ISubscriptionRepository subscriptionRepository) : AttributeChangeHandler<PublicityEventAttribute>
{
    protected override async Task OnDeleteAsync(PublicityEventAttribute deletedAttribute, CalendarEvent calendarEvent)
    {
        var subscriptions = await subscriptionRepository.GetByCalendarEventIdAsync(calendarEvent.Id);
        subscriptionRepository.DeleteRange(subscriptions);
    }
}