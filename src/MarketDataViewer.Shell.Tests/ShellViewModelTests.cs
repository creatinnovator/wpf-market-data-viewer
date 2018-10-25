using MarketDataViewer.Controls.ViewModels;
using MarketDataViewer.Infrastructure;
using MarketDataViewer.Shell.ViewModels;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Reactive.Concurrency;

namespace MarketDataViewer.Shell.Tests
{
    [TestFixture]
    public class ShellViewModelTests
    {
        Mock<IMarketDataService> _marketDataService;

        [SetUp]
        public void Setup()
        {
            _marketDataService = new Mock<IMarketDataService>();
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Changing_ShowAddSymbol_ShouldUpdateAddSymbolViewModel(bool show)
        {
            // arrange
            var stockPricesVm = new StockPricesViewModel(_marketDataService.Object, Scheduler.Immediate);
            var addSymbolVm = new AddSymbolViewModel();

            // act
            var vm = new ShellViewModel(stockPricesVm, addSymbolVm);
            vm.ShowAddSymbol = show;

            // assert
            vm.ShowAddSymbol.ShouldBe(show);
            addSymbolVm.IsVisible.ShouldBe(show);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Changing_AddSymbolIsVisible_ShouldUpdateShowAddSymbol(bool show)
        {
            // arrange
            var stockPricesVm = new StockPricesViewModel(_marketDataService.Object, Scheduler.Immediate);
            var addSymbolVm = new AddSymbolViewModel();

            // act
            var vm = new ShellViewModel(stockPricesVm, addSymbolVm);
            addSymbolVm.IsVisible = show;

            // assert
            vm.ShowAddSymbol.ShouldBe(show);
            addSymbolVm.IsVisible.ShouldBe(show);
        }
    }
}
