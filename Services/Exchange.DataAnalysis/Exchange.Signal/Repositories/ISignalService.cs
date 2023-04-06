using Exchange.Data.Common;
using Exchange.Data.Entities;
using Exchange.Signal.Models;

namespace Exchange.Data.Repositories
{
    public interface ISignalService
    {
        Task<IEnumerable<Signals>> GetAllSignalsAsync(SearchConditionModel model);
        Task<IEnumerable<Signals>> GetAllSignalsByRSIAsync(SearchConditionModel model);
        Task<IEnumerable<Signals>> GetAllSignalsByCrossEMAAsync(SearchConditionModel model);
        Task<IEnumerable<Signals>> GetAllSignalsByTrendLineAsync(SearchConditionModel model);
        Task CalculateSignalsByHistoricalDataAsync(SearchConditionModel model);
    }
}
