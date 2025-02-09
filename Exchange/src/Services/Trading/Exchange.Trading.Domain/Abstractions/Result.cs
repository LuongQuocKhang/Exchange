using Exchange.Trading.Domain.Abstractions.Errors;

namespace Exchange.Trading.Domain.Abstractions;

public class Result<T>
{
    private Result()
    {
        IsSuccess = true;
    }

    private Result(T value)
    {
        Value = value;
        IsSuccess = true;
    }

    private Result(Error? error, Exception? exception)
    {
        Error = error;
        IsSuccess = false;
        Exception = exception;
    }

    public T? Value { get; }

    public Error? Error { get; }

    public bool IsSuccess { get; private set; }

    public Exception? Exception { get; }

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Empty() => new();

    public static Result<T> Failure(Error? error, Exception? exception) => new(error, exception);
}
