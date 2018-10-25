using MarketDataViewer.Controls.ViewModels;
using MarketDataViewer.Infrastructure;
using MarketDataViewer.Infrastructure.Extensions;
using Microsoft.Reactive.Testing;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;

namespace MarketDataViewer.Controls.Tests.ViewModels
{
    [TestFixture]
    public class StockPricesViewModelTests
    {
        private Mock<IMarketDataService> _marketDataService;

        [SetUp]
        public void Setup()
        {
            _marketDataService = new Mock<IMarketDataService>();
        }

        [Test]
        [TestCase("AAA")]
        public void AddingSymbol_ShouldBeSuccessful(string symbol)
        {
            // arrange
            var marketDataStream = new Subject<MarketData>();
            _marketDataService.Setup(x => x.StartSubscription(symbol)).Returns(marketDataStream);

            var vm = new StockPricesViewModel(_marketDataService.Object, Scheduler.Immediate);

            // act
            vm.AddSymbol(symbol);
            // assert
            vm.MarketDataCollection.ShouldNotBeEmpty();
            vm.MarketDataCollection.ShouldContain(x => x.Symbol == symbol && x.LastPrice == 0 && x.ClosingPrice == 0);

            // act
            var marketData = GenerateMarketData(symbol);
            marketDataStream.OnNext(marketData);
            // assert
            vm.MarketDataCollection.ShouldContain(x => 
                x.Symbol == symbol 
                && x.LastPrice == marketData.LastPrice 
                && x.ClosingPrice == marketData.ClosingPrice);
        }

        [Test]
        [TestCase("AAA,BBB")]
        public void AddingMultipleSymbols_ShouldBeSuccessful(string symbols)
        {
            // arrange
            var symbolList = symbols.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var marketDataStream = new Subject<MarketData>();
            symbolList.ForEach(symbol => _marketDataService.Setup(x => x.StartSubscription(symbol)).Returns(marketDataStream));
            
            var vm = new StockPricesViewModel(_marketDataService.Object, Scheduler.Immediate);

            // act
            symbolList.ForEach(symbol => vm.AddSymbol(symbol));
            // assert -- collection should contain the empty market data
            vm.MarketDataCollection.ShouldNotBeEmpty();
            vm.MarketDataCollection.Count.ShouldBe(symbolList.Count());
            vm.MarketDataCollection.ShouldContain(x => symbolList.Contains(x.Symbol));

            // act
            var marketData = symbolList.Select(GenerateMarketData).ToList();
            marketData.ForEach(m => marketDataStream.OnNext(m));
            // assert -- collection should contain updated values
            var expectedUpdatedMarketData = marketData.Select(m => new MarketDataViewModel(m));
            vm.MarketDataCollection.ShouldBe(expectedUpdatedMarketData);
        }

        [Test]
        public void Disposing_ShouldBeSuccessful()
        {
            // arrange

            // act
            var vm = new StockPricesViewModel(_marketDataService.Object, Scheduler.Immediate);
            vm.Dispose();

            // assert
            vm.MarketDataCollection.ShouldBeEmpty();
        }

        private static MarketData GenerateMarketData(string symbol)
        {
            var random = new Random();
            return new MarketData
            {
                Symbol = symbol,
                LastPrice = (decimal)random.NextDouble() * 100,
                ClosingPrice = (decimal)random.NextDouble() * 100,
            };
        }
    }
}
