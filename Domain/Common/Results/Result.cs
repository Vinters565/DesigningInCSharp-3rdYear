namespace SchedulePlanner.Domain.Common.Results;

public class Result<T>
{
    private readonly Error? error;

    private readonly T? value;

    private Result(T value)
    {
        this.value = value;
    }

    private Result(Error error)
    {
        this.error = error;
    }

    public Error Error => error!;

    public string ErrorMessage => error?.Message ?? "";

    public T Value => value!;

    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    public bool IsError => error != null;

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return new Result<T>(error);
    }
}

public class Result
{
    private readonly Error? error;

    private Result()
    {
    }

    private Result(Error error)
    {
        this.error = error;
    }

    public Error Error => error!;

    public string ErrorMessage => error?.Message ?? "";

    public static Result Success()
    {
        return new Result();
    }

    public bool IsError => error != null;

    public static implicit operator Result(Error error)
    {
        return new Result(error);
    }
}