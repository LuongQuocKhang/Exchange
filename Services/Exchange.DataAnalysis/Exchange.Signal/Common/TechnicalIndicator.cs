namespace Exchange.Data.Common
{
    public enum TechnicalIndicator
    {
        RSI = 1,
        TREND_LINE = 2,
        EMA_CROSS = 3, // EMA (34) EMA(89)
        SUPER_TREND = 4 // EMA (34) EMA(89)
    }

    public enum RSI
    {
        OVER_BUY = 70,
        OVER_SELL = 30
    }
}
