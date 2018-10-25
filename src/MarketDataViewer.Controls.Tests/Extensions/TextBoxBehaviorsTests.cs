using MarketDataViewer.Controls.Extensions;
using NUnit.Framework;
using Shouldly;
using System.Windows.Input;

namespace MarketDataViewer.Controls.Tests.Extensions
{
    [TestFixture]
    public class TextBoxBehaviorsTests
    {
        [Test]
        [TestCase(Key.A, true)]
        [TestCase(Key.Z, true)]
        [TestCase(Key.D0, true)]
        [TestCase(Key.D9, true)]
        [TestCase(Key.NumPad0, true)]
        [TestCase(Key.NumPad9, true)]
        [TestCase(Key.D9, true)]
        [TestCase(Key.Space, false)]
        public void IsInputAccepted_ShouldReturnCorrectResult(Key key, bool expected)
        {
            // arrange

            // act
            var result = TextBoxBehaviors.IsInputAccepted(key);

            // assert
            result.ShouldBe(expected);
        }

        [Test]
        [TestCase("A", true)]
        [TestCase("A1", true)]
        [TestCase("1", true)]
        [TestCase("-", false)]
        [TestCase(" ", false)]
        [TestCase("A-", false)]
        [TestCase("A ", false)]
        public void IsInputAccepted_ShouldReturnCorrectResult(string text, bool expected)
        {
            // arrange

            // act
            var result = TextBoxBehaviors.IsInputAccepted(text);

            // assert
            result.ShouldBe(expected);
        }
    }
}
