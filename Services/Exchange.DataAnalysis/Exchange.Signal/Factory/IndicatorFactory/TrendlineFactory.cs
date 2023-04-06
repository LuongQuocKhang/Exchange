using Exchange.Data.Entities;
using Skender.Stock.Indicators;

namespace Exchange.Signal.Factory.IndicatorFactory
{
    public class TrendlineFactory : ISignalFactory
    {
        public IEnumerable<Signals> CalculateSignalsByHistoricalData(IEnumerable<Quote> quotes, string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Signals>> GetSignalsByIndicatorAsync()
        {
            throw new NotImplementedException();
        }
    }
}
