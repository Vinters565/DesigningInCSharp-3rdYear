using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.EventRules;
using SchedulePlanner.Application.Users;
using SchedulePlanner.Domain.EventRules;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<ICalendarEventService, CalendarEventService>();

        services.AddEventRuleChain();

        services.AddScoped<IPasswordHasher, SHA256PasswordHasher>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    private static IServiceCollection AddEventRuleChain(this IServiceCollection services)
    {
        services.AddSingleton<IEventRuleChecker, EventRuleChain>(provider =>
        {
            var calendarEventRepository = provider.GetRequiredService<ICalendarEventRepository>();

            return new EventRuleChain()
                .AddNextEventRule(new MandatoryEventRule())
                .AddNextEventRule(new TimeEventRule())
                .AddNextEventRule(new SingleOnlyEventRule(calendarEventRepository));
        });

        return services;
    }
}
