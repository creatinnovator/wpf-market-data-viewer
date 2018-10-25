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
            var dataStreamTask = marketDataService.StartSubscriptionAsync("AAPL");
            // Since we are still using 7.0, Main() cannot be async, so we have to
            // Wait() on the task manually.
            dataStreamTask.Wait();
            var dataStream = dataStreamTask.Result;
            var subscription = dataStream
                .Subscribe(data =>
                {
                    Console.WriteLine($"Last price: {data.LastPrice}, Closing Price: {data.ClosingPrice}");
                });

            Console.ReadLine();
            subscription.Dispose();
        }
    }
}
