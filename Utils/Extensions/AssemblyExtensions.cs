using System.Reflection;

namespace SchedulePlanner.Utils.Extensions;

public static class AssemblyExtensions
{
    public static Type[] GetTypesByConventionSuffix(this Assembly assembly, string suffix)
    {
        return assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith(suffix))
            .ToArray();
    }

    public static Type[] GetAllImplementationsOf<T>(this Assembly assembly)
    {
        var baseType = typeof(T);
        
        return assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t))
            .ToArray();
    }
}