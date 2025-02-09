using MediatR;

namespace Exchange.BuildingBlock.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
