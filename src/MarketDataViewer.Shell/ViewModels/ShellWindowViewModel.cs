using MarketDataViewer.Controls.ViewModels;

namespace MarketDataViewer.Shell.ViewModels
{
    /// <summary>
    /// Shell window's view model
    /// </summary>
    public class ShellWindowViewModel : ViewModelBase
    {
        public ShellWindowViewModel(
            StockPricesViewModel stockPricesViewModel,
            AddSymbolViewModel addSymbolViewModel)
        {
            StockPricesViewModel = stockPricesViewModel;
            AddSymbolViewModel = addSymbolViewModel;

            AddSymbolViewModel.StockSymbolService = StockPricesViewModel;

            AddPropertyChangedHandler(
                nameof(ShowAddSymbol), 
                () => AddSymbolViewModel.IsVisible = ShowAddSymbol);

            AddSymbolViewModel.AddPropertyChangedHandler(
                nameof(AddSymbolViewModel.IsVisible), 
                () => ShowAddSymbol = AddSymbolViewModel.IsVisible);
        }

        /// <summary>
        /// Stock Prices grid
        /// </summary>
        public StockPricesViewModel StockPricesViewModel { get; }

        /// <summary>
        /// Add symbol view
        /// </summary>
        public AddSymbolViewModel AddSymbolViewModel { get; }

        /// <summary>
        /// Flag whether to show the Add Symbol view or now
        /// </summary>
        public bool ShowAddSymbol { get; set; }
    }
}
