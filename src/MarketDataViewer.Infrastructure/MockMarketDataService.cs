using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace MarketDataViewer.Infrastructure
{
    // TODO Add Unit Test
    /// <summary>
    /// Mock version of our market data service
    /// </summary>
    public class MockMarketDataService : IMarketDataService
    {
        private readonly ConcurrentDictionary<string, Subject<MarketData>> _subscriptions = 
            new ConcurrentDictionary<string, Subject<MarketData>>();
        private readonly Random _randomizer = new Random();

        /// <summary>
        /// Starts the subscription
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public Task<IObservable<MarketData>> StartSubscriptionAsync(string symbol) 
            => Task.FromResult<IObservable<MarketData>>(_subscriptions.GetOrAdd(symbol, CreateSubscription));

        /// <summary>
        /// Stops the subscriptions
        /// </summary>
        /// <param name="symbol"></param>
        public Task StopSubscriptionAsync(string symbol)
        {
            return Task.Run(() =>
            {
                if (_subscriptions.TryRemove(symbol, out Subject<MarketData> symbolDataStream))
                {
                    symbolDataStream.Dispose();
                }
            });
        }

        /// <summary>
        /// Creates RX stream for the given symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private Subject<MarketData> CreateSubscription(string symbol)
        {
            var dataStream = new Subject<MarketData>();
            dataStream.OnNext(GenerateMarketData(symbol));

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    dataStream.OnNext(GenerateMarketData(symbol));
                });

            return dataStream;
        }

        private decimal GeneratePrice() => (decimal)_randomizer.NextDouble() * 200;

        private MarketData GenerateMarketData(string symbol) => new MarketData
        {
            Symbol = symbol,
            LastPrice = GeneratePrice(),
            ClosingPrice = GeneratePrice()
        };
    }
}
