using Exchange.Data.Common;
using Exchange.Data.Entities;
using Exchange.Signal.Factory.IndicatorFactory;

namespace Exchange.Signal.Factory
{
    public static class IndicatorAbstractFactory
    {
        public static ISignalFactory GetIndicatorFactory(TechnicalIndicator indicator)
        {
            switch (indicator)
            {
                case TechnicalIndicator.RSI:
                    return new RsiFactory();
                case TechnicalIndicator.TREND_LINE:
                    return new RsiFactory();
                case TechnicalIndicator.EMA_CROSS:
                    return new CrossEmaFactory();
                case TechnicalIndicator.SUPER_TREND:
                    return new SuperTrendFactory();
                default:
                    return null;
            }
        }
    }
}
