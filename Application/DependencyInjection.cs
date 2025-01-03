using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.AttributesHandlers;
using SchedulePlanner.Application.CalendarEvents.AttributesHandlers.Handlers;
using SchedulePlanner.Application.CalendarEvents.EventAttributes;
using SchedulePlanner.Application.Users;
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

        services.AddSingleton<IAttributeChangeHandler[]>([new PublicityAttributeChangeHandler()]);
        services.AddScoped<IAttributeChangesHandler, AttributeChangesHandler>();
        services.AddScoped<IEventAttributeManager, EventAttributeManager>();

        services.AddScoped<IPasswordHasher, SHA256PasswordHasher>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    private static IServiceCollection AddEventRuleChain(this IServiceCollection services)
    {
        services.AddScoped<IEventRuleChecker, EventRuleChain>(provider =>
        {
            var calendarEventRepository = provider.GetRequiredService<ICalendarEventRepository>();
    
            return new EventRuleChain()
                .AddNextEventRule(new SingleOnlyEventRule(calendarEventRepository))
                .AddNextEventRule(new NonOverlappingLocationsRule(calendarEventRepository));
        });

        return services;
    }
}