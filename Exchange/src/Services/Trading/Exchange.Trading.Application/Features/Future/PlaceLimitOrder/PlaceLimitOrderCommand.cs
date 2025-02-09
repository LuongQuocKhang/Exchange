using Exchange.BuildingBlock.CQRS;

namespace Exchange.Trading.Application.Features.Future.PlaceLimitOrder;

public class PlaceLimitOrderCommand : ICommand<bool>
{
    public string Symbol { get; set; } = string.Empty;

    //public int MyProperty { get; set; }
}
