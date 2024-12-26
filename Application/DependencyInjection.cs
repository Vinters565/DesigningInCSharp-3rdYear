using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.AttributeActions;
using SchedulePlanner.Application.CalendarEvents.EventAttributes;
using SchedulePlanner.Application.CalendarEvents.EventRules;
using SchedulePlanner.Application.CalendarEvents.EventRules.Rules;
using SchedulePlanner.Application.Calendars;

namespace SchedulePlanner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<ICalendarEventService, CalendarEventService>();
        services.AddScoped<ICalendarService, CalendarService>();
        
        services.AddEventRuleChain();

        services.AddSingleton<IAttributeAction[]>([new PublicityAttributeAction()]);
        services.AddScoped<IAttributeActionsApplier, AttributeActionsApplier>();
        services.AddScoped<IEventAttributeManager, EventAttributeManager>();
        
        return services;
    }

    private static IServiceCollection AddEventRuleChain(this IServiceCollection services)
    {
        services.AddSingleton<IEventRuleChecker, EventRuleChain>(provider =>
        {
            var calendarEventRepository = provider.GetRequiredService<ICalendarEventRepository>();
    
            return new EventRuleChain()
                .AddNextEventRule(new SingleOnlyEventRule(calendarEventRepository))
                .AddNextEventRule(new NonOverlappingLocationsRule(calendarEventRepository));
        });

        return services;
    }
}