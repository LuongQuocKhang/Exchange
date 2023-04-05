using Exchange.Data.Entities;
using Exchange.Data.Models;

namespace Exchange.Signal.Factory.IndicatorFactory
{
    public class RsiFactory : ISignalFactory
    {
        public Task<IEnumerable<Signals>> CalculateSignalsByHistoricalData(IEnumerable<Quote> quotes)
        {
            // Query History call data
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Signals>> GetSignalsByIndicatorAsync()
        {
            throw new NotImplementedException();
        }
    }
}
