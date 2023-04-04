using Exchange.Data.Entities;
using Skender.Stock.Indicators;

namespace Exchange.Data.Repositories
{
    public class SignalService : ISignalService
    {
        private readonly ILogger<SignalService> _logger;

        public SignalService(ILogger<SignalService> logger)
        {
            _logger = logger;
        }

        public Task<IEnumerable<Signals>> GetAllSignalsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Signals>> GetAllSignalsByCrossEMA()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Signals>> GetAllSignalsByRSI()
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<Quote>> GetHistoricalData(string symbol, string FromDate = "", string ToDate = "", string interval = "1h")
        {
            var binancePublicAPI = new CCXT.NET.Binance.Public.PublicApi();
            var historicalDataTask = await binancePublicAPI.FetchOHLCVsAsync(base_name: symbol,
                quote_name: "USDT",
                timeframe: interval,
                limits: 200);

            var historicalData = historicalDataTask.result;

            return historicalData.Select(x => new Quote()
            {
                Open = x.openPrice, Close = x.closePrice,
                Date = DateTime.Parse(x.datetime),
                Volume = x.volume,
                High = x.highPrice,
                Low = x.lowPrice
            });
        }
    }
}
