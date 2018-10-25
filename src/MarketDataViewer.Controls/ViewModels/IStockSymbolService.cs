using System.Threading.Tasks;

namespace MarketDataViewer.Controls.ViewModels
{
    /// <summary>
    /// Interface for adding symbol for subscription.
    /// </summary>
    public interface IStockSymbolService
    {
        Task AddSymbolAsync(string symbol);
    }
}
