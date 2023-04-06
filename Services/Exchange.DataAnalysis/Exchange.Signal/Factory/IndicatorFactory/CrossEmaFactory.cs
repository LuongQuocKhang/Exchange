using Exchange.Data.Entities;
using Skender.Stock.Indicators;

namespace Exchange.Signal.Factory.IndicatorFactory
{
    public class CrossEmaFactory : ISignalFactory
    {
        public Task<IEnumerable<Signals>> GetSignalsByIndicatorAsync()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Signals> ISignalFactory.CalculateSignalsByHistoricalData(IEnumerable<Quote> quotes, string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
