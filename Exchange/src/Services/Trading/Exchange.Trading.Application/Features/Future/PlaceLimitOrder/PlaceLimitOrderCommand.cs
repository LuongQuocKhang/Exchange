using Exchange.BuildingBlock.CQRS;
using Exchange.Trading.Application.Constant;

namespace Exchange.Trading.Application.Features.Future.PlaceLimitOrder;

public class PlaceLimitOrderCommand : ICommand<bool>
{
    public string Symbol { get; set; } = string.Empty;

    public double Amount { get; set; }

    public double? TargetPrice { get; set; }

    public OrderPosition Position { get; set; }

    public bool IsTpSlOrder { get; set; }

    public double? TakeProfit { get; set; }

    public double? StopLoss { get; set; }
}
