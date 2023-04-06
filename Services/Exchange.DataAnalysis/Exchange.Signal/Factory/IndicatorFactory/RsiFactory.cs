using Amazon.Runtime.Internal;
using Exchange.Data.Common;
using Exchange.Data.Data;
using Exchange.Data.Entities;
using Skender.Stock.Indicators;

namespace Exchange.Signal.Factory.IndicatorFactory
{
    public class RsiFactory : ISignalFactory
    {
        public IEnumerable<Signals> CalculateSignalsByHistoricalData(IEnumerable<Quote> quotes, string symbol)
        {
            var signal = new Signals()
            {
                CreatedDate = DateTime.Now,
                Indicator = "RSI",
                Description = "",
                Price = 0,
                Symbol = symbol,
                Type = ""
            };
            var rsiResults = quotes.GetRsi();
            foreach (var item in rsiResults)
            {
                if (item.Date == DateTime.Now)
                {
                    
                    if (item.Rsi >= (double)RSI.OVER_BUY)
                    {
                        signal.Type = "SELL";
                        yield return signal;
                    }
                    else if (item.Rsi <= (double)RSI.OVER_SELL)
                    {
                        signal.Type = "BUY";
                        yield return signal;
                    }
                }
            }
        }

        public Task<IEnumerable<Signals>> GetSignalsByIndicatorAsync()
        {
            throw new NotImplementedException();
        }
    }
}
