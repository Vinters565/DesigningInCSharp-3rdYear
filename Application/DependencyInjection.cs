using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.EventRules;
using SchedulePlanner.Application.CalendarEvents.EventRules.Rules;
using SchedulePlanner.Application.Users;
using SchedulePlanner.Utils.Extensions;

namespace SchedulePlanner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddServices(assembly);
        services.AddEventRuleChain();
        services.AddTransient<IPasswordHasher, SHA256PasswordHasher>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, Assembly assembly)
    {
        services.AddByConvention(assembly, "Service", ServiceLifetime.Scoped);
        services.AddByConvention(assembly, "Manager", ServiceLifetime.Scoped);
        services.AddByConvention(assembly, "Handler", ServiceLifetime.Scoped);
        return services;
    }

    private static IServiceCollection AddEventRuleChain(this IServiceCollection services)
    {
        services.AddTransient<IEventRuleChecker, EventRuleChain>(provider =>
        {
            var calendarEventRepository = provider.GetRequiredService<ICalendarEventRepository>();
    
            return new EventRuleChain()
                .AddNextEventRule(new SingleOnlyEventRule(calendarEventRepository))
                .AddNextEventRule(new NonOverlappingLocationsRule(calendarEventRepository));
        });

        return services;
    }
}