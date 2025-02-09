using Exchange.BuildingBlock.Abstractions;
using Exchange.BuildingBlock.CQRS;
using MediatR;

namespace Exchange.BuildingBlock.Behaviors;

public sealed class QueryCachingPipelineBehavior<TRequest, TResponse>(ICacheRepository cacheService)
: IPipelineBehavior<TRequest, TResponse?> where TRequest : ICacheQuery
{
    private readonly ICacheRepository _cacheService = cacheService;

    public async Task<TResponse?> Handle(TRequest request, RequestHandlerDelegate<TResponse?> next, CancellationToken cancellationToken)
    {
        return await _cacheService.GetOrCreateAsync(request.Key,
            _ => next(),
            request.Expiration,
            cancellationToken);
    }
}
