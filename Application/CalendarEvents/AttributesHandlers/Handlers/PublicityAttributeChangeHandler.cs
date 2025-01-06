using SchedulePlanner.Application.Subscriptions;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Application.CalendarEvents.AttributesHandlers.Handlers;

public class PublicityAttributeChangeHandler(
    ISubscriptionRepository subscriptionRepository) : AttributeChangeHandler<PublicityEventAttribute>
{
    protected override Task OnDeleteAsync(PublicityEventAttribute deletedAttribute, CalendarEvent calendarEvent)
    {
        subscriptionRepository.DeleteByCalendarEventId(calendarEvent.Id);
        return Task.CompletedTask;
    }
}