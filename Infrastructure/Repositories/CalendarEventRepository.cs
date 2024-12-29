using SchedulePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Application.CalendarEvents;

namespace SchedulePlanner.Infrastructure.Repositories;

public class CalendarEventRepository : ICalendarEventRepository
{
    private readonly AppDbContext context;

    public CalendarEventRepository(AppDbContext context) => this.context = context;

    public async Task<List<CalendarEvent>> GetAllByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        return await context.CalendarEvents
            .Where(e => e.UserId == userId && e.StartDate >= start && e.EndDate <= end)
            .ToListAsync();
    }

    public async Task<CalendarEvent?> GetByIdAsync(Guid id)
    {
        return await context.CalendarEvents.FindAsync(id);
    }

    public void Delete(CalendarEvent calendarEvent)
    {
        context.CalendarEvents.Remove(calendarEvent);
    }

    public void AddEvent(CalendarEvent newEvent)
    {
        context.CalendarEvents.Add(newEvent);
    }

    public List<CalendarEvent> GetAllEvents()
    {
        return context.CalendarEvents.ToList();
    }

    public void UpdateEvent(CalendarEvent updatedEvent)
    {
        context.CalendarEvents.Update(updatedEvent);
    }

    public void DeleteEventById(string id)
    {
        var eventToDelete = context.CalendarEvents.FirstOrDefault(e => e.Id.ToString() == id);
        if (eventToDelete != null)
        {
            context.CalendarEvents.Remove(eventToDelete);
        }
    }

    public List<CalendarEvent> GetEvents(DateTime start, DateTime end)
    {
        return context.CalendarEvents
            .Where(e => e.StartDate >= start && e.EndDate <= end)
            .ToList();
    }

    public bool Any(DateTime start, DateTime end)
    {
        return context.CalendarEvents
            .Any(e => e.StartDate < end && e.EndDate > start);
    }

    public async Task<bool> AnyWithLocationAsync(string location, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }
}

//public class CalendarEventRepository : ICalendarEventRepository
//{
//    private const string connectionString = "Data Source={calendar_app.db};Version=3;";
//    private readonly JsonSerializerOptions serializeOptions;
//    public CalendarEventRepository()
//    {
//        InitializeDatabase();
//        serializeOptions = new JsonSerializerOptions
//        {
//            WriteIndented = true
//        };
//        serializeOptions.Converters.Add(new EventAttributeIReadOnlyDictionaryConverter());
//        serializeOptions.Converters.Add(new EventAttributeDictionaryConverter());
//    }


//    private void InitializeDatabase()
//    {
//        var command = @"CREATE TABLE IF NOT EXISTS CalendarEvents (
//                Id TEXT PRIMARY KEY,
//                UserId TEXT NOT NULL,
//                StartDate DATETIME NOT NULL,
//                EndDate DATETIME NOT NULL,
//                Attribute TEXT NOT NULL)";

//        ExecuteCommands(command, new Action<SQLiteCommand>(com => { }));
//    }

//    public void DeleteTable()
//    {
//        var command = @"DROP TABLE IF EXISTS CalendarEvents";
//        var commandAction = new Action<SQLiteCommand>(command => command.ExecuteNonQuery());
//        ExecuteCommands(command, commandAction);
//    }

//    public void Delete(CalendarEvent calendarEvent)
//    {
//        DeleteEvent(calendarEvent.Id.ToString());
//    }

//    public void AddEvent(CalendarEvent newEvent)
//    {
//        var command = @"INSERT INTO CalendarEvents (Id, UserId, StartDate, EndDate, Attribute)
//              VALUES (@Id, @UserId, @StartDate, @EndDate, @Attribute)";

//        var commandAction = new Action<SQLiteCommand>(command =>
//        {
//            command.Parameters.AddWithValue("@Id", newEvent.Id.ToString("D"));
//            command.Parameters.AddWithValue("@UserId", newEvent.UserId.ToString("D"));
//            command.Parameters.AddWithValue("@StartDate", newEvent.StartDate);
//            command.Parameters.AddWithValue("@EndDate", newEvent.EndDate);
//            command.Parameters.AddWithValue("@Attribute", JsonSerializer.Serialize(newEvent.AttributeData.Attributes, serializeOptions));
//        });

//        ExecuteCommands(command, commandAction);
//    }

//    public List<CalendarEvent> GetAllEvents()
//    {
//        var command = @"SELECT * FROM CalendarEvents";

//        var funcReader = new Func<SQLiteDataReader, List<CalendarEvent>>(reader =>
//        {
//            var events = new List<CalendarEvent>();
//            while (reader.Read())
//            {
//                events.Add(new CalendarEvent(
//                    Guid.Parse(reader["UserId"].ToString()!),
//                    Guid.Parse(reader["Id"].ToString()!),
//                    Convert.ToDateTime(reader["StartDate"]),
//                    Convert.ToDateTime(reader["EndDate"]),
//                    JsonSerializer.Deserialize<Dictionary<Type, IEventAttribute>>(reader["Attribute"].ToString()!, serializeOptions)
//                        ?? new Dictionary<Type, IEventAttribute>())
//                );
//            }
//            return events;
//        });

//        return ExecuteCommandsReader(command, new Action<SQLiteCommand>( command => { }), funcReader);
//    }

//    public async Task<List<CalendarEvent>> GetAllByUserIdAsync(Guid userId, DateTime start, DateTime end)
//    {
//        var command = @"SELECT * FROM CalendarEvents
//              WHERE UserId = @UserId AND (strftime('%Y-%m-%d %H:%M:%S', StartDate) <= @EndDate)
//              AND (strftime('%Y-%m-%d %H:%M:%S', EndDate) >= @StartDate)";

//        var commandAction = new Action<SQLiteCommand>(command =>
//        {
//            command.Parameters.AddWithValue($"UserId", userId.ToString("D"));
//            command.Parameters.AddWithValue("@StartDate", start.ToString("yyyy-MM-dd HH:mm:ss"));
//            command.Parameters.AddWithValue("@EndDate", end.ToString("yyyy-MM-dd HH:mm:ss"));
//        });

//        var funcReader = new Func<DbDataReader, Task<List<CalendarEvent>>>(async reader =>
//        {
//            var events = new List<CalendarEvent>();
//            while (await reader.ReadAsync())
//            {
//                events.Add(new CalendarEvent(
//                    Guid.Parse(reader["UserId"].ToString()!),
//                    Guid.Parse(reader["Id"].ToString()!),
//                    Convert.ToDateTime(reader["StartDate"]),
//                    Convert.ToDateTime(reader["EndDate"]),
//                    JsonSerializer.Deserialize<Dictionary<Type, IEventAttribute>>(reader["Attribute"].ToString()!, serializeOptions)
//                        ?? new Dictionary<Type, IEventAttribute>())
//                );
//            }
//            return events;
//        });

//        return await ExecuteCommandsReaderAsync(command, commandAction, funcReader, default(CancellationToken));
//    }

//    public async Task<CalendarEvent?> GetByIdAsync(Guid id)
//    {
//        var command = @"SELECT * FROM CalendarEvents
//              WHERE Id = @Id";

//        var commandAction = new Action<SQLiteCommand>(command =>
//        {
//            command.Parameters.AddWithValue($"Id", id.ToString("D"));
//        });

//        var funcReader = new Func<DbDataReader, Task<CalendarEvent>>(async reader =>
//        {
//            await reader.ReadAsync();
//            return new CalendarEvent(
//                    Guid.Parse(reader["UserId"].ToString()!),
//                    Guid.Parse(reader["Id"].ToString()!),
//                    Convert.ToDateTime(reader["StartDate"]),
//                    Convert.ToDateTime(reader["EndDate"]),
//                    JsonSerializer.Deserialize<Dictionary<Type, IEventAttribute>>(reader["Attribute"].ToString()!, serializeOptions)
//                        ?? new Dictionary<Type, IEventAttribute>());
//        });

//        return await ExecuteCommandsReaderAsync(command, commandAction, funcReader, default(CancellationToken));
//    }

//    public void UpdateEvent(CalendarEvent updatedEvent)
//    {
//        var command = @"UPDATE CalendarEvents
//              SET StartDate = @StartDate, EndDate = @EndDate, Attribute = @Attribute
//              WHERE Id = @Id";

//        var commandAction = new Action<SQLiteCommand>(command =>
//        {
//            command.Parameters.AddWithValue("@Id", updatedEvent.Id.ToString("D"));
//            command.Parameters.AddWithValue("@StartDate", updatedEvent.StartDate);
//            command.Parameters.AddWithValue("@EndDate", updatedEvent.EndDate);
//            command.Parameters.AddWithValue("@Attribute", JsonSerializer.Serialize(updatedEvent.AttributeData.Attributes, serializeOptions));
//        });

//        ExecuteCommands(command, commandAction);
//    }

//    public void DeleteEvent(string id)
//    {
//        var command = @"DELETE FROM CalendarEvents WHERE Id = @Id";

//        var commandAction = new Action<SQLiteCommand>(command =>
//        {
//            command.Parameters.AddWithValue("@Id", id);
//            command.ExecuteNonQuery();
//        });

//        ExecuteCommands(command, commandAction);
//    }


//    public List<CalendarEvent> GetEvents(DateTime start, DateTime end)
//    {
//        var command = @"SELECT * FROM CalendarEvents 
//            WHERE (strftime('%Y-%m-%d %H:%M:%S', StartDate) <= @EndDate)
//            AND (strftime('%Y-%m-%d %H:%M:%S', EndDate) >= @StartDate)";

//        var commandAction = new Action<SQLiteCommand>(command =>
//        {
//            command.Parameters.AddWithValue("@StartDate", start.ToString("yyyy-MM-dd HH:mm:ss"));
//            command.Parameters.AddWithValue("@EndDate", end.ToString("yyyy-MM-dd HH:mm:ss"));
//        });

//        var funcReader = new Func<SQLiteDataReader, List<CalendarEvent>>(reader =>
//        {
//            var events = new List<CalendarEvent>();
//            while (reader.Read())
//            {
//                events.Add(new CalendarEvent(
//                    Guid.Parse(reader["UserId"].ToString()!),
//                    Guid.Parse(reader["Id"].ToString()!),
//                    Convert.ToDateTime(reader["StartDate"]),
//                    Convert.ToDateTime(reader["EndDate"]),
//                    JsonSerializer.Deserialize<Dictionary<Type, IEventAttribute>>(reader["Attribute"].ToString()!, serializeOptions)
//                        ?? new Dictionary<Type, IEventAttribute>())
//                );
//            }
//            return events;
//        });

//        return ExecuteCommandsReader(command, commandAction, funcReader);
//    }

//    public bool Any(DateTime start, DateTime end)
//    {
//        return GetEvents(start, end).Count > 0;
//    }

//    public async Task<bool> AnyWithLocationAsync(string location, DateTime start, DateTime end)
//    {
//        throw new NotImplementedException();
//    }

//    private void ExecuteCommands(string sqlCommand, Action<SQLiteCommand> action)
//    {
//        using var connection = new SQLiteConnection(connectionString);
//        connection.Open();
//        using var command = new SQLiteCommand(sqlCommand, connection);
//        action(command);
//        command.ExecuteNonQuery();
//    }

//    private T ExecuteCommandsReader<T>(string sqlCommand, Action<SQLiteCommand> action, Func<SQLiteDataReader, T> readFunc)
//    {
//        using var connection = new SQLiteConnection(connectionString);
//        connection.Open();
//        using var command = new SQLiteCommand(sqlCommand, connection);
//        action(command);
//        command.ExecuteNonQuery();
//        using var reader = command.ExecuteReader();
//        return readFunc(reader);
//    }

//    private async Task<T> ExecuteCommandsReaderAsync<T>(
//        string sqlCommand,
//        Action<SQLiteCommand> configureCommand,
//        Func<DbDataReader, Task<T>> readFunc,
//        CancellationToken cancellationToken)
//    {
//        await using var connection = new SQLiteConnection(connectionString);
//        await connection.OpenAsync(cancellationToken);

//        await using var command = new SQLiteCommand(sqlCommand, connection);
//        configureCommand(command);

//        await using var reader = await command.ExecuteReaderAsync();
//        return await readFunc(reader);
//    }
//}