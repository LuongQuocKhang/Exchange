namespace Exchange.BuildingBlock.CQRS;

/// <summary>
/// Use memory cache to cache query
/// </summary>
/// <typeparam name="TResponse"></typeparam>

public interface ICachedQuery<out TResponse> : IQuery<TResponse>, ICacheQuery
    where TResponse : notnull
{
}

public interface ICacheQuery
{
    public string Key { get; }

    public TimeSpan? Expiration { get; set; }
}
