using Exchange.Data.Common;
using Exchange.Data.Entities;
using Exchange.Signal.Factory.IndicatorFactory;

namespace Exchange.Signal.Factory
{
    public static class IndicatorAbstractFactory
    {
        public static ISignalFactory GetIndicatorFactory(Indicator indicator)
        {
            switch (indicator)
            {
                case Indicator.RSI:
                    return new RsiFactory();
                case Indicator.TREND_LINE:
                    return new RsiFactory();
                case Indicator.EMA_CROSS:
                    return new CrossEmaFactory();
                case Indicator.SUPER_TREND:
                    return new SuperTrendFactory();
                default:
                    return null;
            }
        }
    }
}
