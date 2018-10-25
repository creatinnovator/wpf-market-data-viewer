using System.Windows.Controls;

namespace MarketDataViewer.Controls.Views
{
    /// <summary>
    /// Interaction logic for AddSymbolView.xaml
    /// </summary>
    public partial class AddSymbolView : UserControl, IAddSymbolView
    {
        public AddSymbolView()
        {
            InitializeComponent();
        }

        public void SetSymbol(string symbol)
        {
            SymbolTextBox.Text += symbol;
            SymbolTextBox.CaretIndex = symbol.Length;
        }
    }
}
