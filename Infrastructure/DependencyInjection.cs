using Microsoft.Extensions.DependencyInjection;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Infrastructure.Repositories;

namespace SchedulePlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddTransient<ICalendarEventRepository, CalendarEventRepository>();

        return services;
    }
}