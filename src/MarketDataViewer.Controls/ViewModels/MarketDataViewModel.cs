namespace MarketDataViewer.Controls.ViewModels
{
    /// <summary>
    /// View model equivalent of MarketData object. 
    /// This will be useful so only updated fields/properties will fire the PropertyChanged
    /// </summary>
    public class MarketDataViewModel : ViewModelBase
    {
        public string Symbol { get; set; }
        public decimal LastPrice { get; set; }
        public decimal ClosingPrice { get; set; }
    }
}
