using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SchedulePlanner.Utils.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllImplementationsOf<T>(this IServiceCollection services, 
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var baseType = typeof(T);

        var assembly = Assembly.GetAssembly(baseType);

        if (assembly == null)
            throw new InvalidOperationException($"Cannot find assembly for type {baseType.FullName}");

        var implementations = assembly.GetAllImplementationsOf<T>();

        foreach (var implementation in implementations)
        {
            services.Add(new ServiceDescriptor(baseType, implementation, lifetime));
        }

        return services;
    }
    
    public static IServiceCollection AddByConvention(this IServiceCollection services, 
        Assembly assembly, 
        string suffix, 
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var typesToRegister = assembly.GetTypesByConventionSuffix(suffix);

        foreach (var type in typesToRegister)
        {
            var typeInterfaces = type.GetInterfaces()
                .Where(t => t.Name.EndsWith(suffix));
            
            foreach (var typeInterface in typeInterfaces)
            {
                services.Add(new ServiceDescriptor(typeInterface, type, lifetime));
            }
        }

        return services;
    }
}