using Prism.Commands;
using System.ComponentModel;

namespace MarketDataViewer.Controls.ViewModels
{
    /// <summary>
    /// View model for the Add Symbol dialog/view
    /// </summary>
    public class AddSymbolViewModel : ViewModelBase
    {
        public AddSymbolViewModel()
        {
            AddSymbolCommand = new DelegateCommand(AddSymbol, CanAddSymbol);
            IsVisible = false;

            AddPropertyChangedHandler(
                nameof(IsVisible), 
                () => Symbol = string.Empty);
        }

        /// <summary>
        /// Service where to add/register the symbol.
        /// Instead of this, we can also use Pub/Sub for sending Symbol message to further promote loose coupling.
        /// </summary>
        public IStockSymbolService StockSymbolService { private get; set; }

        /// <summary>
        /// True to make the associated view visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Symbol specified by the user
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Command for adding the symbol
        /// </summary>
        public DelegateCommand AddSymbolCommand { get; }

        /// <summary>
        /// Returns true if the Symbol is not null/empty
        /// </summary>
        /// <returns></returns>
        private bool CanAddSymbol() => !string.IsNullOrEmpty(Symbol);

        /// <summary>
        /// Add the symbol via the symbol service.
        /// </summary>
        private void AddSymbol()
        {
            StockSymbolService.AddSymbol(Symbol);
            IsVisible = false;
        }

        /// <summary>
        /// Since Prism doesnt raise the CanExecuteChanged implicitly (other library does, like DevExpress's command)
        /// we have to manually raise that event for property changes that affects the state of the Command.CanExecute().
        /// </summary>
        /// <param name="args"></param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            AddSymbolCommand.RaiseCanExecuteChanged();
        }
    }
}
