# wpf-market-data-viewer
Prototype/sample app for viewing market data using C#/WPF.

## Technology
Technology used are as follows:
- .NET 4.6.1
- C# 7.0
- WPF
- async/await
- Reactive eXtensions (RX)
- Prism
- Unity for DI/IoC
- fody, propertychanged.fody - IL weaving of raising property changed event
- Unit Tests
  - NUnit - unit test framework
  - Moq - mocking framework
  - Shouldly - fluent assertion
 
## Components
### MarketDataViewer.Infrastructure
Contains infrastructure related tools/utilities, including
- Custom LINQ extensions
- Market Data Service (interface, mocked implementation)
  - Uses async/await to subcribe/unsubscribe to/from a market data
  - Uses RX (Reactive Extensions) to stream market data updates

### MarketDataViewer.Controls
Contains UI elements/controls used for displaying stock prices
- `AddSymbolView/ViewModel` - components that allows users to specify which stock prices will be monitored
- `StockPricesView/ViewModel` - components for viewing stock prices
- `IStockSymbolService` - interface for setting the symbol specified by the user. This is implemented by Stock Price VM. When user clicks OK or presses Enter, Add Symbol View Model calls `IStockSymbolService.AddSymbol()` to register the symbol. StockPricesViewModel then gets a subscription for that symbol from the Market Data Service.
- Behaviors
  - Visibility - shows/hides a control that implements `IAddSymbolView` depending on the key stroke
    - Shows the control if alpha-numeric is keyed in
    - Hides the control if Escape is keyed in 
  - Text Box Alphanumeric Validation - filters input and allows alphanumeric characters only
  - Text Box Focus - focuses on a control once the control becomes visible. Useful for the textbox, so that when the Add Symbol view is shown, keyboard focus will be set to the text box.
  
### MarketDataViewer.Shell
Contains the Shell application that hosts the stock price controls/views.
  
### MarketDataViewer.Cmd
Command-line app for viewing prices of a single stock.

## Possible Improvements
- Use Pub/Sub such as Prism's `EventAggregator` to further promote loose coupling but still allow communication between ViewModels and other components 
