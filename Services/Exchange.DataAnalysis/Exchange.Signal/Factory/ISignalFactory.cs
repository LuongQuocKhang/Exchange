using Exchange.Data.Entities;
using Skender.Stock.Indicators;

namespace Exchange.Signal.Factory
{
    public interface ISignalFactory
    {
        Task<IEnumerable<Signals>> GetSignalsByIndicatorAsync();
        IEnumerable<Signals> CalculateSignalsByHistoricalData(IEnumerable<Quote> quotes, string symbol);
    }
}
