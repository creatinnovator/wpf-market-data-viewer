using System.Windows.Controls;
using System.Windows.Input;

namespace MarketDataViewer.Controls.Views
{
    /// <summary>
    /// Interaction logic for StockPricesView.xaml
    /// </summary>
    public partial class StockPricesView : UserControl
    {
        public StockPricesView()
        {
            InitializeComponent();
        }

        private void StockPricesView_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"StockPricesView: {e.Key}");
        }
    }
}
