using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.EventRules;
using SchedulePlanner.Application.CalendarEvents.EventRules.Rules;

namespace SchedulePlanner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<ICalendarEventService, CalendarEventService>();
            
        services.AddEventRuleChain();
        
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