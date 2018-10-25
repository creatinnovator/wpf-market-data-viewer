using System;

namespace MarketDataViewer.Infrastructure
{
    /// <summary>
    /// Interface for managing symbol subscription
    /// </summary>
    public interface IMarketDataService
    {
        IObservable<MarketData> StartSubscription(string symbol);
        void StopSubscription(string symbol);
    }
}
