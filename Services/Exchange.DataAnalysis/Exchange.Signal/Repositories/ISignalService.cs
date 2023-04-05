using Exchange.Data.Entities;
using Exchange.Signal.Models;

namespace Exchange.Data.Repositories
{
    public interface ISignalService
    {
        Task<IEnumerable<Signals>> GetAllSignalsAsync();
        Task<IEnumerable<Signals>> GetAllSignalsByRSI(SearchConditionModel model);
        Task<IEnumerable<Signals>> GetAllSignalsByCrossEMA(SearchConditionModel model);
        Task<IEnumerable<Signals>> GetAllSignalsByTrendLine(SearchConditionModel model);
    }
}
