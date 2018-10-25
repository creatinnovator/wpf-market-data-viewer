using MarketDataViewer.Controls.Converters;
using NUnit.Framework;
using Shouldly;
using System.Globalization;
using System.Windows;

namespace MarketDataViewer.Controls.Tests.Converters
{
    [TestFixture]
    public class BooleanToVisibilityConverterTests
    {
        [Test]
        [TestCase(true, Visibility.Visible)]
        [TestCase(false, Visibility.Collapsed)]
        public void Converting_FromBooleanToVisibility_ShouldBeSuccessful(bool input, Visibility expected)
        {
            // arrange

            // act
            var converter = new BooleanToVisibilityConverter();
            var result = converter.Convert(input, typeof(Visibility), null, CultureInfo.InvariantCulture);

            // assert
            result.ShouldBe(expected);
        }

        [Test]
        [TestCase(Visibility.Visible, true)]
        [TestCase(Visibility.Collapsed, false)]
        [TestCase(Visibility.Hidden, false)]
        public void Converting_FromVisibilityToBoolean_ShouldBeSuccessful(Visibility input, bool expected)
        {
            // arrange

            // act
            var converter = new BooleanToVisibilityConverter();
            var result = converter.ConvertBack(input, typeof(bool), null, CultureInfo.InvariantCulture);

            // assert
            result.ShouldBe(expected);
        }
    }
}
