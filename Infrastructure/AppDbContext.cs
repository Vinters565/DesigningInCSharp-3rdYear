using System.Text.Json;
using SchedulePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Application.JsonConverters;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CalendarEvent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();

                var serializeOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                serializeOptions.Converters.Add(new EventAttributeIReadOnlyDictionaryConverter());
                serializeOptions.Converters.Add(new EventAttributeDictionaryConverter());

                entity.Property(e => e.AttributeData)
                    .HasColumnName("Attributes")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v.Attributes, serializeOptions),
                        v => new AttributeData(
                            JsonSerializer.Deserialize<Dictionary<Type, IEventAttribute>>(v, serializeOptions) ??
                            new Dictionary<Type, IEventAttribute>())
                    );
            });
        }
    } 
}
