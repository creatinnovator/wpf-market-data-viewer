using MarketDataViewer.Controls.ViewModels;

namespace MarketDataViewer.Shell.ViewModels
{
    /// <summary>
    /// Shell window's view model
    /// </summary>
    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel(
            StockPricesViewModel stockPricesViewModel,
            AddSymbolViewModel addSymbolViewModel)
        {
            StockPricesViewModel = stockPricesViewModel;
            AddSymbolViewModel = addSymbolViewModel;

            AddSymbolViewModel.StockSymbolService = StockPricesViewModel;
        }

        /// <summary>
        /// Stock Prices grid
        /// </summary>
        public StockPricesViewModel StockPricesViewModel { get; }

        /// <summary>
        /// Add symbol view
        /// </summary>
        public AddSymbolViewModel AddSymbolViewModel { get; }
    }
}
