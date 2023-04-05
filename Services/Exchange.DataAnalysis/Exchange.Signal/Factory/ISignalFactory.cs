using Exchange.Data.Entities;
using Exchange.Data.Models;

namespace Exchange.Signal.Factory
{
    public interface ISignalFactory
    {
        Task<IEnumerable<Signals>> GetSignalsByIndicatorAsync();
        Task<IEnumerable<Signals>> CalculateSignalsByHistoricalData(IEnumerable<Quote> quotes);
    }
}
