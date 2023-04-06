using Exchange.Data.Data;
using Exchange.Data.Entities;
using Exchange.Signal.Factory;
using Exchange.Signal.Models;
using Skender.Stock.Indicators;

namespace Exchange.Data.Repositories
{
    public class SignalService : ISignalService
    {
        private readonly ILogger<SignalService> _logger;
        private readonly ISignalContext _context;

        public SignalService(ILogger<SignalService> logger, ISignalContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task CalculateSignalsByHistoricalDataAsync(SearchConditionModel model)
        {
            var historicalData = await GetHistoricalDataAsync(model);

            var indicatorFactory = IndicatorAbstractFactory.GetIndicatorFactory(model.indicator);
            var signals = indicatorFactory.CalculateSignalsByHistoricalData(historicalData, model.Symbol);

            if(signals.Any())
            {
                await _context.Signals.InsertManyAsync(signals);
            }
        }

        public Task<IEnumerable<Signals>> GetAllSignalsAsync(SearchConditionModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Signals>> GetAllSignalsByCrossEMAAsync(SearchConditionModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Signals>> GetAllSignalsByRSIAsync(SearchConditionModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Signals>> GetAllSignalsByTrendLineAsync(SearchConditionModel model)
        {
            throw new NotImplementedException();
        }


        private async Task<IEnumerable<Quote>> GetHistoricalDataAsync(SearchConditionModel model)
        {
            var binancePublicAPI = new CCXT.NET.Binance.Public.PublicApi();
            var historicalDataTask = await binancePublicAPI.FetchOHLCVsAsync(base_name: model.Symbol,
                quote_name: model.Quote_Name,
                timeframe: model.Interval,
                limits: 200);

            var historicalData = historicalDataTask.result;

            return historicalData.Select(x => new Quote()
            {
                Open = x.openPrice,
                Close = x.closePrice,
                Date = DateTime.Parse(x.datetime),
                Volume = x.volume,
                High = x.highPrice,
                Low = x.lowPrice
            });
        }
    }
}
