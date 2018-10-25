namespace MarketDataViewer.Infrastructure
{
    /// <summary>
    /// Market Data
    /// </summary>
    public class MarketData
    {
        public string Symbol { get; set; }
        public decimal LastPrice { get; set; }
        public decimal ClosingPrice { get; set; }
    }
}
