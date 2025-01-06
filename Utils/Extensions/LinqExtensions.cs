namespace SchedulePlanner.Utils.Extensions;

public static class LinqExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source), "Source cannot be null.");
        }

        if (action == null)
        {
            throw new ArgumentNullException(nameof(action), "Action cannot be null.");
        }

        foreach (var item in source)
        {
            action(item);
        }
    }
    
    public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> action)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source), "Source cannot be null.");
        }

        if (action == null)
        {
            throw new ArgumentNullException(nameof(action), "Action cannot be null.");
        }

        foreach (var item in source)
        {
            await action(item);
        }
    }
}