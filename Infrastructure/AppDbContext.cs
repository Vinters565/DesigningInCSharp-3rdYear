using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Domain.ValueTypes;
using SchedulePlanner.Infrastructure.ValueConverters;

namespace SchedulePlanner.Infrastructure
{
    public class AppDbContext : DbContext
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<JsonSerializerOptions> jsonSerializerOptions) 
            : base(options)
        {
            this.jsonSerializerOptions = jsonSerializerOptions.Value;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.OwnsOne(u => u.Settings, settings =>
                {
                    settings.Property(s => s.DisplayedName).IsRequired();
                    settings.Property(s => s.PrimaryColor).HasConversion(new ColorValueConverter());
                    settings.Property(s => s.SecondaryColor).HasConversion(new ColorValueConverter());
                });
            });

            modelBuilder.Entity<CalendarEvent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();

                entity.Property(e => e.AttributeData)
                    .HasColumnName("Attributes")
                    .HasConversion(new AttributeDataValueConverter(jsonSerializerOptions));
            });
            
            base.OnModelCreating(modelBuilder);
        }
    } 
}
