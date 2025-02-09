namespace Exchange.BuildingBlock.Exceptions;

[Serializable]
public class AccessDeniedException : Exception
{
    protected AccessDeniedException()
    {
    }

    protected AccessDeniedException(string? message) : base(message)
    {
    }

    protected AccessDeniedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
