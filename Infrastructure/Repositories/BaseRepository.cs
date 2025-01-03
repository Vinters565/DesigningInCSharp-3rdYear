namespace SchedulePlanner.Infrastructure.Repositories;

public abstract class BaseRepository(AppDbContext context)
{
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}