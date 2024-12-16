using SchedulePlanner.Application.Converters;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;
using System.Data.SQLite;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchedulePlanner.Infrastructure.Repositories;

public class CalendarEventRepository : ICalendarEventRepository
{
    private const string connectionString = "Data Source={calendar_app.db};Version=3;";
    private readonly JsonSerializerOptions serializeOptions;
    public CalendarEventRepository()
    {
        InitializeDatabase();
        serializeOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        serializeOptions.Converters.Add(new EventAttributeIReadOnlyDictionaryConverter());
        serializeOptions.Converters.Add(new EventAttributeDictionaryConverter());
    }


    private void InitializeDatabase()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS CalendarEvents (
                    Id TEXT PRIMARY KEY,
                    StartDate TEXT NOT NULL,
                    EndDate TEXT NOT NULL,
                    Attribute TEXT NOT NULL
                )";

            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public void AddEvent(CalendarEvent newEvent)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string insertQuery = "INSERT INTO CalendarEvents (Id, StartDate, EndDate, Attribute) VALUES (@Id, @StartDate, @EndDate, @Attribute)";
            using (var command = new SQLiteCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", newEvent.Id.ToString());
                command.Parameters.AddWithValue("@StartDate", newEvent.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"));
                command.Parameters.AddWithValue("@EndDate", newEvent.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"));
                command.Parameters.AddWithValue("@Attribute", JsonSerializer.Serialize(newEvent.Attributes, serializeOptions));
                
                command.ExecuteNonQuery();
            }
        }
    }

    public List<CalendarEvent> GetAllEvents()
    {
        var events = new List<CalendarEvent>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string selectQuery = "SELECT * FROM CalendarEvents";
            using (var command = new SQLiteCommand(selectQuery, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    events.Add(new CalendarEvent(
                        Guid.Parse(reader["Id"].ToString()!),
                        Convert.ToDateTime(DateTime.Parse(reader["StartDate"].ToString()!)),
                        Convert.ToDateTime(DateTime.Parse(reader["EndDate"].ToString()!)),
                        JsonSerializer.Deserialize<Dictionary<Type, IEventAttribute>>(reader["Attribute"].ToString()!, serializeOptions)?? new Dictionary<Type, IEventAttribute>())
                    );
                }
            }
        }

        return events;
    }

    public void UpdateEvent(CalendarEvent updatedEvent)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string updateQuery = @"
                UPDATE CalendarEvents
                SET StartDate = @StartDate, EndDate = @EndDate, Attribute = @Attribute
                WHERE Id = @Id";

            using (var command = new SQLiteCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", updatedEvent.Id.ToString());
                command.Parameters.AddWithValue("@StartDate", updatedEvent.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"));
                command.Parameters.AddWithValue("@EndDate", updatedEvent.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"));
                command.Parameters.AddWithValue("@Attribute", "");

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteEvent(string id)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string deleteQuery = "DELETE FROM CalendarEvents WHERE Id = @Id";
            using (var command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }

    
    public CalendarEvent[] GetEvents(DateTime start, DateTime end)
    {
        var events = new List<CalendarEvent>();
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string deleteQuery = "SELECT * FROM CalendarEvents WHERE StartDate > @StartDate or StartDate < @EndDate";
            using (var command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@StartDate", start.ToString("yyyy-MM-ddTHH:mm:ss"));
                command.Parameters.AddWithValue("@EndDate", end.ToString("yyyy-MM-ddTHH:mm:ss"));
                command.ExecuteNonQuery();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new CalendarEvent(
                            Guid.Parse(reader["Id"].ToString()!),
                            Convert.ToDateTime(DateTime.Parse(reader["StartDate"].ToString()!)),
                            Convert.ToDateTime(DateTime.Parse(reader["EndDate"].ToString()!)))
                        );
                    }
                }
            }
        }
        return events.ToArray();
    }

    public bool Any(DateTime start, DateTime end)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string findQuery = "SELECT * FROM CalendarEvents WHERE StartDate > @StartDate or StartDate < @EndDate";
            using (var command = new SQLiteCommand(findQuery, connection))
            {
                command.Parameters.AddWithValue("@StartDate", start.ToString("yyyy-MM-ddTHH:mm:ss"));
                command.Parameters.AddWithValue("@EndDate", end.ToString("yyyy-MM-ddTHH:mm:ss"));
                command.ExecuteNonQuery();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
    }
}