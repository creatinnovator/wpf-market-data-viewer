using MarketDataViewer.Controls.ViewModels;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace MarketDataViewer.Controls.Tests.ViewModels
{
    [TestFixture]
    public class AddSymbolViewModelTests
    {
        private Mock<IStockSymbolService> _stockSymbolService;

        [SetUp]
        public void Setup()
        {
            _stockSymbolService = new Mock<IStockSymbolService>();
        }

        [Test]
        [TestCase(true, false, "AAA", "")] // hide
        [TestCase(false, true, "AAA", "")] // show
        public void Changing_IsVisible_ShouldResetSymbol(bool isVisibleInitialValue, 
            bool isVisibleNextValue, string symbolInitialValue, string symbolExpectedValue)
        {
            // arrange
            var vm = new AddSymbolViewModel
            {
                IsVisible = isVisibleInitialValue,
                Symbol = symbolInitialValue
            };

            // act
            vm.IsVisible = isVisibleNextValue;

            // assert
            vm.Symbol.ShouldBe(symbolExpectedValue);
        }

        [Test]
        [TestCase("AAA", true)]
        [TestCase("", false)]
        public void CanAddSymbol_ShouldBeCorrect(string symbol, bool expected)
        {
            // arrange
            
            // act
            var vm = new AddSymbolViewModel { Symbol = symbol };

            // assert
            vm.AddSymbolCommand.CanExecute().ShouldBe(expected);
        }

        [Test]
        [TestCase("AAA")]
        public void AddSymbol_ShouldCorrectlyAddSymbol(string symbol)
        {
            // arrange
            _stockSymbolService.Setup(x => x.AddSymbol(It.Is<string>(s => s == symbol)));

            // act
            var vm = new AddSymbolViewModel
            {
                Symbol = symbol,
                StockSymbolService = _stockSymbolService.Object
            };
            vm.AddSymbolCommand.Execute();

            // assert
            _stockSymbolService.Verify(x => x.AddSymbol(It.Is<string>(s => s == symbol)));
            vm.IsVisible.ShouldBe(false);
        }
    }
}
