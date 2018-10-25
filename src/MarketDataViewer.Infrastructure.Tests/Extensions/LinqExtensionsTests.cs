using MarketDataViewer.Infrastructure.Extensions;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace MarketDataViewer.Infrastructure.Tests.Extensions
{
    [TestFixture]
    public class LinqExtensionsTests
    {
        [Test]
        public void ForEach_ShouldExecuteAllForItems()
        {
            // arrange
            var items = new string[] { "AAA", "BBB" }.AsEnumerable();
            var processedItems = new List<string>();

            // act
            items.ForEach(x => processedItems.Add(x));

            // assert
            processedItems.ShouldBe(items);
        }
    }
}
