using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Infrastructure.Repositories;

namespace SchedulePlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddRepository();

        return services;
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddSingleton<ICalendarEventRepository, CalendarEventRepository>(provider => new CalendarEventRepository());

        return services;
    }
}