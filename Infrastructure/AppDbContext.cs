using SchedulePlanner.Domain;
using SchedulePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchedulePlanner.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
