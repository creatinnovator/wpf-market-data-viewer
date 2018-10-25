using MarketDataViewer.Infrastructure;
using System;

namespace MarketDataViewer.Cmd
{
    /// <summary>
    /// Console app for displaying market data, testing the RX observables
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var marketDataService = new MockMarketDataService();
            var subscription = marketDataService.StartSubscription("AAPL")
                .Subscribe(data =>
                {
                    Console.WriteLine($"Last price: {data.LastPrice}, Closing Price: {data.ClosingPrice}");
                });

            Console.ReadLine();
            subscription.Dispose();
        }
    }
}
