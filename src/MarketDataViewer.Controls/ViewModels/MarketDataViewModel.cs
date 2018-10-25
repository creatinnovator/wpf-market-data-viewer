using MarketDataViewer.Infrastructure;
using System;

namespace MarketDataViewer.Controls.ViewModels
{
    /// <summary>
    /// View model equivalent of MarketData object. 
    /// This will be useful so only updated fields/properties will fire the PropertyChanged
    /// </summary>
    public class MarketDataViewModel : ViewModelBase, IEquatable<MarketDataViewModel>
    {
        public MarketDataViewModel(MarketData marketData)
        {
            Symbol = marketData.Symbol;
            LastPrice = marketData.LastPrice;
            ClosingPrice = marketData.ClosingPrice;
        }

        public string Symbol { get; }
        public decimal LastPrice { get; private set; }
        public decimal ClosingPrice { get; private set; }

        public void Update(MarketData marketData)
        {
            LastPrice = marketData.LastPrice;
            ClosingPrice = marketData.ClosingPrice;
        }

        public bool Equals(MarketDataViewModel other)
        {
            return other != null
                && other.Symbol == Symbol
                && other.LastPrice == LastPrice
                && other.ClosingPrice == ClosingPrice;
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && Equals(obj as MarketDataViewModel);
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode()
                ^ LastPrice.GetHashCode()
                ^ ClosingPrice.GetHashCode();
        }
    }
}
