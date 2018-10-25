using MarketDataViewer.Controls.Extensions;
using NUnit.Framework;
using Shouldly;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace MarketDataViewer.Controls.Tests.Extensions
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class VisibilityBehaviorTests
    {
        [Test]
        [TestCase(Key.A, Visibility.Visible)]
        [TestCase(Key.Z, Visibility.Visible)]
        [TestCase(Key.D0, Visibility.Visible)]
        [TestCase(Key.D9, Visibility.Visible)]
        [TestCase(Key.NumPad0, Visibility.Visible)]
        [TestCase(Key.NumPad9, Visibility.Visible)]
        [TestCase(Key.Space, Visibility.Collapsed)]
        [TestCase(Key.Return, Visibility.Collapsed)]
        [TestCase(Key.Escape, Visibility.Collapsed)]
        public void ElementShouldBeVisible_IfInputIsAlphaNumeric(Key key, Visibility expected)
        {
            // arrange

            // act
            var elementToShow = new FrameworkElement { Visibility = Visibility.Collapsed };
            VisibilityBehaviors.HandleKeyDown(elementToShow, key);

            // assert
            elementToShow.Visibility.ShouldBe(expected);
        }
    }
}
