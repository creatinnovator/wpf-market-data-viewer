using MarketDataViewer.Controls.ViewModels;
using MarketDataViewer.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace MarketDataViewer.Controls.Tests.ViewModels
{
    [TestFixture]
    public class MarketDataViewModelTests
    {
        [Test]
        [TestCase("AAA", 123.45, 234.56)]
        public void CreatingMarketDataViewModel_ShouldBeSuccessful(string symbol, decimal lastPrice, decimal closingPrice)
        {
            // arrange
            var marketData = new MarketData { Symbol = symbol, LastPrice = lastPrice, ClosingPrice = closingPrice };

            // act
            var vm = new MarketDataViewModel(marketData);

            // assert
            vm.Symbol.ShouldBe(symbol);
            vm.LastPrice.ShouldBe(lastPrice);
            vm.ClosingPrice.ShouldBe(closingPrice);
        }

        [Test]
        [TestCase("AAA", 123.45, 234.56)]
        public void UpdatingMarketDataViewModel_ShouldBeSuccessful(string symbol, 
            decimal lastPrice, 
            decimal closingPrice)
        {
            // arrange
            var marketData = new MarketData { Symbol = symbol, LastPrice = 0, ClosingPrice = 0 };

            // act
            var vm = new MarketDataViewModel(marketData);
            marketData.LastPrice = lastPrice;
            marketData.ClosingPrice = closingPrice;
            vm.Update(marketData);

            // assert
            vm.Symbol.ShouldBe(symbol);
            vm.LastPrice.ShouldBe(lastPrice);
            vm.ClosingPrice.ShouldBe(closingPrice);
        }
    }
}
