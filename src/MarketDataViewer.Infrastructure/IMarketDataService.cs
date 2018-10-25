using System;
using System.Threading.Tasks;

namespace MarketDataViewer.Infrastructure
{
    /// <summary>
    /// Interface for managing symbol subscription
    /// </summary>
    public interface IMarketDataService
    {
        Task<IObservable<MarketData>> StartSubscriptionAsync(string symbol);
        Task StopSubscriptionAsync(string symbol);
    }
}
