namespace SchedulePlanner.Domain.Common;

public abstract class Entity<TId>(TId id)
{
    public TId Id { get; init; } = id;
}