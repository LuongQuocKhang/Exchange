namespace Exchange.Trading.Domain.Abstractions.Errors;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error SqlException => new("Error.SqlException", $"Database error occurred.");

    public static Error UnexpectedError => new("Error.UnexpectedError", $"An unexpected error occurred.");
}
