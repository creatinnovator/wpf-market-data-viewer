﻿using System.Reactive.Concurrency;
using System.Windows;
using MarketDataViewer.Controls.ViewModels;
using MarketDataViewer.Infrastructure;
using MarketDataViewer.Shell.ViewModels;
using MarketDataViewer.Shell.Views;
using Prism.Ioc;
using Prism.Unity;

namespace MarketDataViewer.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMarketDataService, MockMarketDataService>();
            containerRegistry.Register<AddSymbolViewModel>();
            containerRegistry.Register<StockPricesViewModel>();
            containerRegistry.Register<ShellViewModel>();
            containerRegistry.RegisterInstance<IScheduler>(DispatcherScheduler.Current);
        }

        protected override Window CreateShell()
        {
            var shellViewModel = Container.Resolve<ShellViewModel>();

            return new ShellWindow { DataContext = shellViewModel };
        }
    }
}
