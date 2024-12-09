namespace SchedulePlanner.Domain.Common.Results;

public class Error
{
    private Error(string message)
    {
        Message = message;
    }

    public string Message { get; }

    public static Error Failure(string message) => new(message);
}