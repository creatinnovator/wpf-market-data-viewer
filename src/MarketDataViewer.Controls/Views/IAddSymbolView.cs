using System.Windows;

namespace MarketDataViewer.Controls.Views
{
    public interface IAddSymbolView
    {
        Visibility Visibility { get; set; }

        void SetSymbol(string symbol);
    }
}
