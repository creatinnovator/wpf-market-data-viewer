using MarketDataViewer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using MarketDataViewer.Infrastructure.Extensions;

namespace MarketDataViewer.Controls.ViewModels
{
    /// <summary>
    /// View model for the Stock Price view grid
    /// </summary>
    public class StockPricesViewModel : ViewModelBase, IStockSymbolService, IDisposable
    {
        /// <summary>
        /// All individual per-symbol subscription we did
        /// </summary>
        private readonly Dictionary<string, IDisposable> _subscriptions;

        /// <summary>
        /// Market data visible in the grid
        /// </summary>
        private readonly Dictionary<string, MarketDataViewModel> _mappedMarketData;

        /// <summary>
        /// Market Data Service that accepts symbol registration
        /// </summary>
        private readonly IMarketDataService _marketDataService;
        private readonly IScheduler _observeOnScheduler;

        public StockPricesViewModel(IMarketDataService marketDataService, IScheduler observeOnScheduler)
        {
            _marketDataService = marketDataService;
            _observeOnScheduler = observeOnScheduler;
            _subscriptions = new Dictionary<string, IDisposable>();
            _mappedMarketData = new Dictionary<string, MarketDataViewModel>();

            MarketDataCollection = new ObservableCollection<MarketDataViewModel>();
        }

        /// <summary>
        /// Collection of market data
        /// </summary>
        public ObservableCollection<MarketDataViewModel> MarketDataCollection { get; }

        /// <summary>
        /// Adds the symbol for subscription.
        /// </summary>
        /// <param name="symbol"></param>
        public void AddSymbol(string symbol)
        {
            if (!_subscriptions.ContainsKey(symbol))
            {
                // if the symbol is not yet subscribed,

                // - add empty market data
                UpdateMarketData(new MarketData { Symbol = symbol });

                // - start the subscription via the market data service
                var subscription = _marketDataService.StartSubscription(symbol);
                var disposableSubscription = subscription
                    .ObserveOn(_observeOnScheduler)
                    .Subscribe(UpdateMarketData);

                // - save the RX subscription for later disposal
                _subscriptions.Add(symbol, disposableSubscription);
            }
        }

        /// <summary>
        /// Handles the update from RX stream
        /// </summary>
        /// <param name="marketData"></param>
        private void UpdateMarketData(MarketData marketData)
        {
            if (!_mappedMarketData.ContainsKey(marketData.Symbol))
            {
                // If market data is not yet in our map, we create and add the equivalent View model
                var mappedMarketData = new MarketDataViewModel(marketData);

                _mappedMarketData.Add(marketData.Symbol, mappedMarketData);
                MarketDataCollection.Add(mappedMarketData);
            }
            else
            {
                // Since we already have the VM market data, we just update the properties.
                // We could also use AutoMapper here instead of manually updating each field.
                var mappedMarketData = _mappedMarketData[marketData.Symbol];
                mappedMarketData.Update(marketData);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose each subscription we have
                    _subscriptions.ForEach(s =>
                    {
                        MarketDataCollection.Clear();
                        // stop subscription for the symbol
                        _marketDataService.StopSubscription(s.Key); 
                        // dispose existing subscription
                        s.Value.Dispose();
                    });
                    _subscriptions.Clear();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
