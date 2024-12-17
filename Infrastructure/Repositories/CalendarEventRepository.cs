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
        ExecuteCommands(
            @"CREATE TABLE IF NOT EXISTS CalendarEvents (
                Id TEXT PRIMARY KEY,
                UserId TEXT NOT NULL,
                StartDate DATETIME NOT NULL,
                EndDate DATETIME NOT NULL,
                Attribute TEXT NOT NULL)",
            new Action<SQLiteCommand>(com => { })
            );
    }

    public void DeleteTable()
    {
        ExecuteCommands(
            "DROP TABLE IF EXISTS CalendarEvents",
            new Action<SQLiteCommand>(command => command.ExecuteNonQuery())
            );
    }

    public void AddEvent(CalendarEvent newEvent)
    {
        ExecuteCommands(
            @"INSERT INTO CalendarEvents (Id, UserId, StartDate, EndDate, Attribute)
              VALUES (@Id, @UserId, @StartDate, @EndDate, @Attribute)",
            new Action<SQLiteCommand>(command =>
            {
                command.Parameters.AddWithValue("@Id", newEvent.Id.ToString("D"));
                command.Parameters.AddWithValue("@UserId", newEvent.UserId.ToString("D"));
                command.Parameters.AddWithValue("@StartDate", newEvent.StartDate);
                command.Parameters.AddWithValue("@EndDate", newEvent.EndDate);
                command.Parameters.AddWithValue("@Attribute", JsonSerializer.Serialize(newEvent.Attributes, serializeOptions));
            }));
    }

    public List<CalendarEvent> GetAllEvents()
    {
        return ExecuteCommandsReader(
            "SELECT * FROM CalendarEvents",
            new Action<SQLiteCommand>( command => { }),
            new Func<SQLiteDataReader, List<CalendarEvent>>(reader =>
            {
                var events = new List<CalendarEvent>();
                while (reader.Read())
                {
                    events.Add(new CalendarEvent(
                        Guid.Parse(reader["UserId"].ToString()!),
                        Guid.Parse(reader["Id"].ToString()!),
                        Convert.ToDateTime(reader["StartDate"]),
                        Convert.ToDateTime(reader["EndDate"]),
                        JsonSerializer.Deserialize<Dictionary<Type, IEventAttribute>>(reader["Attribute"].ToString()!, serializeOptions)
                            ?? new Dictionary<Type, IEventAttribute>())
                    );
                }
                return events;
            }));
    }

    public void UpdateEvent(CalendarEvent updatedEvent)
    {
        ExecuteCommands(
            @"UPDATE CalendarEvents
              SET StartDate = @StartDate, EndDate = @EndDate, Attribute = @Attribute
              WHERE Id = @Id",
            new Action<SQLiteCommand>(command =>
            {
                command.Parameters.AddWithValue("@Id", updatedEvent.Id.ToString("D"));
                command.Parameters.AddWithValue("@StartDate", updatedEvent.StartDate);
                command.Parameters.AddWithValue("@EndDate", updatedEvent.EndDate);
                command.Parameters.AddWithValue("@Attribute", JsonSerializer.Serialize(updatedEvent.Attributes, serializeOptions));
            }));
    }

    public void DeleteEvent(string id)
    {
        ExecuteCommands(
            "DELETE FROM CalendarEvents WHERE Id = @Id",
            new Action<SQLiteCommand>(command =>
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }));
    }

    
    public List<CalendarEvent> GetEvents(DateTime start, DateTime end)
    {
        return ExecuteCommandsReader(
            @"SELECT * FROM CalendarEvents WHERE (strftime('%Y-%m-%d %H:%M:%S', StartDate) <= @EndDate)
            AND (strftime('%Y-%m-%d %H:%M:%S', EndDate) >= @StartDate)",
            new Action<SQLiteCommand>(command => 
            {
                command.Parameters.AddWithValue("@StartDate", start.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@EndDate", end.ToString("yyyy-MM-dd HH:mm:ss"));
            }),
            new Func<SQLiteDataReader, List<CalendarEvent>>(reader =>
            {
                var events = new List<CalendarEvent>();
                while (reader.Read())
                {
                    events.Add(new CalendarEvent(
                        Guid.Parse(reader["UserId"].ToString()!),
                        Guid.Parse(reader["Id"].ToString()!),
                        Convert.ToDateTime(reader["StartDate"]),
                        Convert.ToDateTime(reader["EndDate"]),
                        JsonSerializer.Deserialize<Dictionary<Type, IEventAttribute>>(reader["Attribute"].ToString()!, serializeOptions)
                            ?? new Dictionary<Type, IEventAttribute>())
                    );
                }
                return events;
            }
            ));
    }

    public bool Any(DateTime start, DateTime end)
    {
        return GetEvents(start, end).Count > 0;
    }

    private void ExecuteCommands(string sqlCommand, Action<SQLiteCommand> action)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sqlCommand, connection))
            {
                action(command);
                command.ExecuteNonQuery();
            }
        }
    }

    private T ExecuteCommandsReader<T>(string sqlCommand, Action<SQLiteCommand> action, Func<SQLiteDataReader, T> readFunc)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sqlCommand, connection))
            {
                action(command);
                command.ExecuteNonQuery();
                using (var reader = command.ExecuteReader())
                {
                    return readFunc(reader);
                }
            }
        }
    }
}