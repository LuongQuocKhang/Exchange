using Exchange.Data.Entities;
using Exchange.Signal.Factory;
using Exchange.Signal.Models;
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

        public async Task<IEnumerable<Signals>> GetAllSignalsByCrossEMA(SearchConditionModel model)
        {
            var crossFactory = IndicatorAbstractFactory.GetIndicatorFactory(Common.Indicator.EMA_CROSS);
            var signals = await crossFactory.GetSignalsByIndicatorAsync();
            return signals;
        }

        public async Task<IEnumerable<Signals>> GetAllSignalsByRSI(SearchConditionModel model)
        {
            var rsiFactory = IndicatorAbstractFactory.GetIndicatorFactory(Common.Indicator.RSI);
            var signals = await rsiFactory.GetSignalsByIndicatorAsync();
            return signals;
        }

        public async Task<IEnumerable<Signals>> GetAllSignalsByTrendLine(SearchConditionModel model)
        {
            var trendlineFactory = IndicatorAbstractFactory.GetIndicatorFactory(Common.Indicator.EMA_CROSS);
            var signals = await trendlineFactory.GetSignalsByIndicatorAsync();
            return signals;
        }

        private async Task<IEnumerable<Quote>> GetHistoricalData(SearchConditionModel model)
        {
            var binancePublicAPI = new CCXT.NET.Binance.Public.PublicApi();
            var historicalDataTask = await binancePublicAPI.FetchOHLCVsAsync(base_name: model.Symbol,
                quote_name: model.Quote_Name,
                timeframe: model.Interval,
                limits: 200);

            var historicalData = historicalDataTask.result;

            return historicalData.Select(x => new Quote()
            {
                Open = x.openPrice, Close = x.closePrice,
                Date = DateTime.Parse(x.datetime),
                Volume = x.volume,
                High = x.highPrice,
                Low = x.lowPrice
            }).Where(x => x.Date >= DateTime.Parse(model.FromDate)
            && x.Date <= DateTime.Parse(model.ToDate));
        }
    }
}
