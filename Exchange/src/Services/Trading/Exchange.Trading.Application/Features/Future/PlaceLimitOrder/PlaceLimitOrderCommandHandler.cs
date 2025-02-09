using Exchange.BuildingBlock.CQRS;

namespace Exchange.Trading.Application.Features.Future.PlaceLimitOrder;

internal class PlaceLimitOrderCommandHandler : ICommandHandler<PlaceLimitOrderCommand, bool>
{
    public Task<bool> Handle(PlaceLimitOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
