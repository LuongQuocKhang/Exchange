using Exchange.Data.Entities;

namespace Exchange.Data.Repositories
{
    public interface ISignalService
    {
        Task<IEnumerable<Signals>> GetAllSignalsAsync();
        Task<IEnumerable<Signals>> GetAllSignalsByRSI();
        Task<IEnumerable<Signals>> GetAllSignalsByCrossEMA();
        //Task<IEnumerable<Signals>> GetAllSignalsByCrossEMA();
    }
}
