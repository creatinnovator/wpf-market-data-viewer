using MarketDataViewer.Controls.Extensions;
using MarketDataViewer.Controls.Views;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Windows;
using System.Windows.Input;

namespace MarketDataViewer.Controls.Tests.Extensions
{
    public class VisibilityBehaviorTests
    {
        private Mock<IAddSymbolView> _addSymbolView;

        [SetUp]
        public void Setup()
        {
            _addSymbolView = new Mock<IAddSymbolView>();
        }

        [Test]
        [TestCase(Key.A, Visibility.Visible)]
        [TestCase(Key.Z, Visibility.Visible)]
        [TestCase(Key.D0, Visibility.Visible)]
        [TestCase(Key.D9, Visibility.Visible)]
        [TestCase(Key.NumPad0, Visibility.Visible)]
        [TestCase(Key.NumPad9, Visibility.Visible)]
        [TestCase(Key.Escape, Visibility.Collapsed)]
        public void ElementShouldBeVisible_IfInputIsAlphaNumeric(Key key, Visibility expected)
        {
            // arrange
            _addSymbolView.Setup(x => x.Visibility).Returns(Visibility.Collapsed);
            _addSymbolView.Setup(x => x.SetSymbol(It.Is<string>(s => s == ConvertToString(key))));

            // act
            VisibilityBehaviors.DoShowWhenKeystroke(_addSymbolView.Object, key);

            // assert
            _addSymbolView.VerifySet(x => x.Visibility = expected); 
        }

        [Test]
        [TestCase(Key.Space)]
        [TestCase(Key.Return)]
        public void ShouldNotProcessIfKeystrokeIsNonAlphaNumeric(Key key)
        {
            // arrange
            _addSymbolView.Setup(x => x.Visibility).Returns(Visibility.Collapsed);
            _addSymbolView.Setup(x => x.SetSymbol(It.Is<string>(s => s == ConvertToString(key))));

            // act
            VisibilityBehaviors.DoShowWhenKeystroke(_addSymbolView.Object, key);

            // assert
            _addSymbolView.VerifySet(x => x.Visibility = Visibility.Collapsed, Times.Never);
            _addSymbolView.VerifySet(x => x.Visibility = Visibility.Visible, Times.Never);
        }

        [Test]
        [TestCase(Key.D0, "0")]
        [TestCase(Key.D9, "9")]
        [TestCase(Key.NumPad0, "0")]
        [TestCase(Key.NumPad9, "9")]
        [TestCase(Key.A, "A")]
        [TestCase(Key.Z, "Z")]
        [TestCase(Key.Return, "")]
        public void ConvertToChar_ShouldBeSuccessful(Key key, string expected)
        {
            // act
            var result = VisibilityBehaviors.ConvertToChar(key);

            // assert
            result.ShouldBe(expected);
        }

        private static string ConvertToString(Key key)
        {
            return VisibilityBehaviors.ConvertToChar(key);
        }
    }
}
